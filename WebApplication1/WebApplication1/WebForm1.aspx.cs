using SODA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;





namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static Dictionary<int, string> UNIQUE_CATEGORIES;
        public static DataTable API_RESULTS;

        protected void Page_Load(object sender, EventArgs e)
        {
            initiateCSS();
            //getData();
            createImageButtons();
            
            if (!Page.IsPostBack)
            {
                getParentData();
            }

            
        }

        private string callDiscoveryAPI()
        {
            var url = "http://api.us.socrata.com/api/catalog/v1";

           if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
           {
               url =  url + "?q=" + txtSearch.Text.Trim().ToLower();
           }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            String sResponse = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                sResponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

            return sResponse;
        }

        private static DataTable getAvailableAPIs(string sResponse)
        {
            dynamic dyn = JsonConvert.DeserializeObject(sResponse);
            DataTable dt = new DataTable();
            DataColumn col;
            DataRow row;

            #region Column
            col = new DataColumn();
            col.ColumnName = "Id";
            col.DataType = System.Type.GetType("System.String");
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "UpdateDate";
            col.Caption = "Update Date";
            col.DataType = System.Type.GetType("System.DateTime");
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "Name";
            col.Caption = "Name";
            col.DataType = System.Type.GetType("System.String");
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "Description";
            col.Caption = "Description";
            col.DataType = System.Type.GetType("System.String");
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "Categories";
            col.Caption = "Categories";
            col.DataType = System.Type.GetType("System.String");
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "Link";
            col.Caption = "Link";
            col.DataType = System.Type.GetType("System.String");
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "Domain";
            col.Caption = "Domain";
            col.DataType = System.Type.GetType("System.String");
            col.ReadOnly = true;
            dt.Columns.Add(col);

            col = new DataColumn();
            col.ColumnName = "ResourceId";
            col.Caption = "4x4";
            col.DataType = System.Type.GetType("System.String");
            col.ReadOnly = true;
            dt.Columns.Add(col);
            #endregion
            
            foreach (var obj in dyn)
            {
                var results = dyn.results;
                var resultSetSize = dyn.resultSetSize; //optional data
                var timings = dyn.timings; //optional data
                var count = 1; 
                foreach (var resObj in results)
                {
                    row = dt.NewRow();
                    row["Id"] = count ++;
                    row["UpdateDate"] = Convert.ToDateTime(resObj.resource.updatedAt);
                    row["Name"] = resObj.resource.name;
                    row["Description"] = resObj.resource.description;
                    row["Categories"] = createAPICategories(resObj.classification.categories);
                    row["Link"] = resObj.link;
                    row["Domain"] = resObj.metadata.domain;;
                    row["ResourceId"] = resObj.resource.id;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        private static string createAPICategories(dynamic categories)
        {
            var catResult = string.Empty;
            

            foreach (var cat in categories)
            {
                if (!string.IsNullOrEmpty(catResult))
                {
                    catResult = catResult + ", ";
                }
                catResult = catResult + cat.Value.Trim();
            }

            return catResult.Trim();
        }

        private static void createFilters(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                #region categories
                var categories = dr["Categories"].ToString().Split(',');
                foreach (string category in categories)
                {
                    var cat = (!string.IsNullOrEmpty(category) ? category.Trim().ToUpper() : "NO CATEGORY - BLANK");

                    if (!UNIQUE_CATEGORIES.ContainsValue(cat))
                    {
                        var count = UNIQUE_CATEGORIES.Count() + 1;
                        UNIQUE_CATEGORIES.Add(count, cat);
                    }

                }
                #endregion
            }

        }

        private void bindCategory()
        {
            ddlCategories.Items.Clear();

            UNIQUE_CATEGORIES.Add(0, "-- SELECT A CATEGORY --");
            var sortedCategories = UNIQUE_CATEGORIES.OrderBy(x => x.Value);
            
            ddlCategories.DataSource = sortedCategories;
            ddlCategories.DataTextField = "Value";
            ddlCategories.DataValueField = "Key";
            ddlCategories.DataBind();
            ddlCategories.SelectedValue = "0";
        }

        private void bindDiscoveredAPI()
        {
            discoveredAPI.DataSource = API_RESULTS;
            discoveredAPI.DataBind();
        }

        private void getParentData()
        {
            UNIQUE_CATEGORIES = new Dictionary<int, string>();
            API_RESULTS = new DataTable();

            clearDetailData();

            var sResponse = callDiscoveryAPI();
            API_RESULTS = getAvailableAPIs(sResponse);

            litResultCount.Text = API_RESULTS.Rows.Count.ToString();

            createFilters(API_RESULTS);

            #region page control binders;
            bindCategory();
            bindDiscoveredAPI();
            #endregion
        }

        private void getDetailData(string url, string fourbyfour)
        {
            var appToken = "09sIcqEhoY0teGY5rhupZGqhW";
            //var url = "data.ct.gov";
            //var fourbyfour = "v4tt-nt9n";

            //var url = "data.consumerfinance.gov";
            //var fourbyfour = "jhzv-w97w";
            //initialize a new client
            //make sure you register for your own app token (http://dev.socrata.com/register)
            var client = new SodaClient(url, appToken);
            //read metadata of a dataset using the resource identifier (Socrata 4x4)
            var metadata = client.GetMetadata(fourbyfour);
            //get a reference to the resource itself
            //the result (a Resouce object) is a generic type
            //the type parameter represents the underlying rows of the resource
            var dataset = client.GetResource<Dictionary<string, object>>(fourbyfour);

            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"description\">" + metadata.Description + "</div>");
            sb.Append("<div style=\"overflow-x:auto;\"><table>");
            sb.Append("<tr>");
            sb.Append(createHeader(metadata.Columns));
            sb.Append("</tr>");
            sb.Append(createRows(metadata.Columns, dataset));
            sb.Append("</table></div>");

            results.InnerHtml = Server.HtmlDecode(sb.ToString());
        }

        private void clearDetailData() 
        {
            results.InnerHtml = Server.HtmlDecode("<h2>select a master data on the grid above</h2>");
        }

        private void initiateCSS()
        {
            // Define an HtmlLink control.
            HtmlLink myHtmlLink = new HtmlLink();
            myHtmlLink.Href = "~/StyleSheet1.css";
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");

            // Add the HtmlLink to the Head section of the page.
            Page.Header.Controls.Add(myHtmlLink);

        }

        private static string createHeader(IEnumerable<ResourceColumn> columns)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ResourceColumn col in columns)
            {
                sb.Append("<th>" + col.Name + "</th>");
            }

            return sb.ToString();
        }

        private static string createRows(IEnumerable<ResourceColumn> columns, Resource<Dictionary<string, object>> dataset)
        {
            StringBuilder sb = new StringBuilder();
            ////Resource objects read their own data
            var allRows = dataset.GetRows(10);

            foreach (Dictionary<string, object> r in allRows)
            {
                sb.Append("<tr>");
                foreach (ResourceColumn c in columns)
                {
                    var colFound = false;
                    foreach (KeyValuePair<string, object> i in r)
                    {
                        if (c.SodaFieldName.Equals(i.Key))
                        {
                            sb.Append("<td>" + i.Value + "</td>");
                            colFound = true;
                            break;
                        }
                    }

                    if (!colFound)
                    {
                        sb.Append("<td></td>");
                    }
                }

                sb.Append("</tr>");
            }

            return sb.ToString();
        }


        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCurrentGridData();
            discoveredAPI.CurrentPageIndex = 0;
            discoveredAPI.DataBind();
        }

        protected void Grid_Change(Object sender, DataGridPageChangedEventArgs e)
        {
            // Set CurrentPageIndex to the page the user clicked.
            discoveredAPI.CurrentPageIndex = e.NewPageIndex;
            // Rebind the data to refresh the DataGrid control. 
            setCurrentGridData();
            discoveredAPI.DataBind();
        }

        private void setCurrentGridData()
        {
            clearDetailData();

            if (ddlCategories.SelectedItem.Text.ToLower().Trim().Contains("no category - blank"))
            {
                discoveredAPI.DataSource = (from myRows in API_RESULTS.AsEnumerable()
                                            where string.IsNullOrEmpty(myRows.Field<string>("Categories").Trim())
                                            select myRows).AsDataView();
            }
            else if (ddlCategories.SelectedItem.Text.ToLower().Trim().Contains("-- select a category --"))
            {
                discoveredAPI.DataSource = API_RESULTS;
            }
            else
            {
                discoveredAPI.DataSource = (from myRows in API_RESULTS.AsEnumerable()
                                            where myRows.Field<string>("Categories").ToLower().Contains(ddlCategories.SelectedItem.Text.ToLower())
                                            select myRows).AsDataView();
            }
        }

        protected void discoveredAPI_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (ddlCategories.SelectedItem.Text.ToLower().Trim().Contains("no category - blank"))
            {
                discoveredAPI.DataSource = (from myRows in API_RESULTS.AsEnumerable()
                                            where string.IsNullOrEmpty(myRows.Field<string>("Categories").Trim())
                                            orderby myRows.Field<string>(e.SortExpression)
                                            select myRows).AsDataView();
            }
            else if (ddlCategories.SelectedItem.Text.ToLower().Trim().Contains("-- select a category --"))
            {
                discoveredAPI.DataSource = (from myRows in API_RESULTS.AsEnumerable()
                                            orderby myRows.Field<string>(e.SortExpression)
                                            select myRows).AsDataView();
            }
            else
            {
                discoveredAPI.DataSource = (from myRows in API_RESULTS.AsEnumerable()
                                            where myRows.Field<string>("Categories").ToLower().Contains(ddlCategories.SelectedItem.Text.ToLower())
                                            orderby myRows.Field<string>(e.SortExpression)
                                            select myRows).AsDataView();
            }
            
            discoveredAPI.DataBind();
        }

       protected void grvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Do wherever you want with grvSearch.SelectedIndex                
            }
            catch
            {
                //...throw
            }
        }

        protected void discoveredAPI_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    // Retrieve the text of the CurrencyColumn from the DataGridItem
                    // and convert the value to a Double.
                    var cellValue = e.Item.Cells[2].Text;
                }
            }
            catch
            {
                //...throw
            }
        }

        protected void discoveredAPI_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                var url = e.Item.Cells[7].Text.Trim();
                var resourceId = e.Item.Cells[8].Text.Trim();
                getDetailData(url, resourceId);
            }
            catch { }
        }

        protected void btnSeach_Click(object sender, EventArgs e)
        {
            getParentData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            getParentData();
        }

        private void createImageButtons()
        {
            var divMyButtons = FindControl("myButtons");

            if (divMyButtons != null)
            {
                var imageLocation = "~/images/imageButtons/";
                string[] fileEntries = Directory.GetFiles(Server.MapPath(imageLocation));
                foreach (string fileName in fileEntries)
                {
                    FileInfo info = new FileInfo(fileName);
                    var name = info.Name;
                    var imageButton = new ImageButton();
                    imageButton.ID = "btn" + Common.UppercaseFirst(name.Substring(0, name.IndexOf('.')));
                    imageButton.ImageUrl = imageLocation + name;
                    imageButton.Click += new ImageClickEventHandler(this.imgButton_Click);
                    imageButton.AlternateText = name;
                    imageButton.ToolTip = name;
                    divMyButtons.Controls.Add(imageButton);
                }
            }
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            var imgButton = (ImageButton)sender;
            if (string.IsNullOrEmpty(imgButton.CssClass))
            {
                imgButton.CssClass = "inactive";
            }
            else
            {
                imgButton.CssClass = string.Empty;
            }
            
          
        }

    }
}