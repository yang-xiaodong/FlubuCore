
//-----------------------------------------------------------------------
// <auto-generated />
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using FlubuCore.Context;
using FlubuCore.Tasks;
using FlubuCore.Tasks.Process;

namespace FlubuCore.Tasks.Docker.Node
{
     public partial class DockerNodePsTask : ExternalProcessTaskBase<int, DockerNodePsTask>
     {
        private string[] _node;

        
        public DockerNodePsTask(params string[] node)
        {
            ExecutablePath = "docker";
            WithArguments("node ps");
_node = node;

        }

        protected override string Description { get; set; }
        
        /// <summary>
        /// Filter output based on conditions provided
        /// </summary>
        public DockerNodePsTask Filter(string filter)
        {
            WithArgumentsValueRequired("filter", filter.ToString());
            return this;
        }

        /// <summary>
        /// Pretty-print tasks using a Go template
        /// </summary>
        public DockerNodePsTask Format(string format)
        {
            WithArgumentsValueRequired("format", format.ToString());
            return this;
        }

        /// <summary>
        /// Do not map IDs to Names
        /// </summary>
        public DockerNodePsTask NoResolve()
        {
            WithArguments("no-resolve");
            return this;
        }

        /// <summary>
        /// Do not truncate output
        /// </summary>
        public DockerNodePsTask NoTrunc()
        {
            WithArguments("no-trunc");
            return this;
        }

        /// <summary>
        /// Only display task IDs
        /// </summary>
        public DockerNodePsTask Quiet()
        {
            WithArguments("quiet");
            return this;
        }
        protected override int DoExecute(ITaskContextInternal context)
        {
             WithArguments(_node);

            return base.DoExecute(context);
        }

     }
}
