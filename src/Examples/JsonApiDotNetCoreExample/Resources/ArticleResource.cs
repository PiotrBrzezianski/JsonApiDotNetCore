using System.Collections.Generic;
using System.Linq;
using System;
using JsonApiDotNetCore.Internal;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCoreExample.Models;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace JsonApiDotNetCoreExample.Resources
{
    public class ArticleResource : ResourceDefinition<Article>
    {
        public ArticleResource(IResourceGraph graph) : base(graph) { }

        public override IEnumerable<Article> OnReturn(HashSet<Article> entities, ResourcePipeline pipeline)
        {
            if (pipeline == ResourcePipeline.GetSingle && entities.Single().Name == "Classified")
            {
                throw new JsonApiException(403, "You are not allowed to see this article!", new UnauthorizedAccessException());
            }
            return entities.Where(t => t.Name != "This should be not be included");
        }
    }
}

