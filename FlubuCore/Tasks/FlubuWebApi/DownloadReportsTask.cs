﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FlubuCore.Context;
using FlubuCore.WebApi.Client;
using FlubuCore.WebApi.Model;

namespace FlubuCore.Tasks.FlubuWebApi
{
    public class DownloadReportsTask : WebApiBaseTask<DownloadReportsTask, int>
    {
        private readonly string _saveAs;
        private string _subDirectory = null;
        private string _description;

        public DownloadReportsTask(IWebApiClientFactory webApiClient, string saveAs)
            : base(webApiClient)
        {
            _saveAs = saveAs;
        }

        protected override string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_description))
                {
                    return $"Downloads reports from flubu server.";
                }

                return _description;
            }

            set { _description = value; }
        }

        public DownloadReportsTask DownloadromSubDirectory(string subDirectory)
        {
            _subDirectory = subDirectory;
            return this;
        }

        protected override int DoExecute(ITaskContextInternal context)
        {
            Task<int> task = DoExecuteAsync(context);

            return task.GetAwaiter().GetResult();
        }

        protected override async Task<int> DoExecuteAsync(ITaskContextInternal context)
        {
            if (string.IsNullOrEmpty(_saveAs))
            {
                throw new ArgumentNullException(nameof(_saveAs));
            }

            var extension = Path.GetExtension(_saveAs);

            if (string.IsNullOrEmpty(extension) || !extension.Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                throw new TaskExecutionException("SaveAs file extension must be .zip", 99);
            }

            var client = WebApiClientFactory.Create(context.Properties.Get<string>(BuildProps.LastWebApiBaseUrl));
            var reports = await client.DownloadReportsAsync(new DownloadReportsRequest()
            {
                DownloadFromSubDirectory = _subDirectory
            }) as MemoryStream;

            FileStream file = new FileStream(_saveAs, FileMode.Create, FileAccess.Write);
            reports.WriteTo(file);

            #if !NETSTANDARD1_6

            reports.Close();
            file.Close();
            #endif
            return 0;
        }
    }
}
