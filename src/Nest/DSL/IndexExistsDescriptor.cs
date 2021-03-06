﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[DescriptorFor("IndicesExists")]
	public partial class IndexExistsDescriptor : IndexPathDescriptorBase<IndexExistsDescriptor, IndexExistsQueryString>
		, IPathInfo<IndexExistsQueryString>
	{
		ElasticsearchPathInfo<IndexExistsQueryString> IPathInfo<IndexExistsQueryString>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo<IndexExistsQueryString>(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;

			return pathInfo;
		}
	}
}
