﻿using System;
using System.IO;
using System.Web.Hosting;

namespace AzureJobs.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private static string folder = HostingEnvironment.MapPath("~/img/");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            foreach (string file in Directory.EnumerateFiles(folder, "*.*"))
            {
                string src = "/img/" + Path.GetFileName(file);
                long size = new FileInfo(file).Length;
                divImages.InnerHtml += string.Format("<img src='{0}' title='{1} bytes' />", src, size);
            }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            if (!files.HasFiles)
                return;

            foreach (var file in files.PostedFiles)
            {
                string path = Path.Combine(folder, Guid.NewGuid() + Path.GetExtension(file.FileName));
                file.SaveAs(path);
                Response.Redirect(Request.Path, true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Directory.Delete(folder, true);
            Directory.CreateDirectory(folder);
            Response.Redirect(Request.Path, true);
        }
    }
}