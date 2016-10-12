﻿using System;
using FlubuCore.Context;
using FlubuCore.Scripting;
using Microsoft.Extensions.Logging;

namespace FlubuCore.Context
{
    public class TaskContext : ITaskContext
    {
        private readonly ILogger _log;

        private bool _disposed;

        private int _executionDepth;

        public TaskContext(ILogger log, ITaskContextSession taskContextProperties, CommandArguments args)
        {
            _log = log;
            Args = args;
            Properties = taskContextProperties;
        }

        public ITaskContextSession Properties { get; }

        public CommandArguments Args { get; }

        public bool IsInteractive { get; set; } = true;

        public void IncreaseDepth()
        {
            _executionDepth++;
        }

        public void LogInfo(string message)
        {
            _log?.LogInformation(message);
        }

        public void LogError(string message)
        {
            _log?.LogError(message);
        }

        public void DecreaseDepth()
        {
            _executionDepth--;
        }

        public void Fail(string message, int errorCode = 0)
        {
            LogError(message);
            throw new TaskExecutionException(message, errorCode);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            _disposed = true;
        }
    }
}