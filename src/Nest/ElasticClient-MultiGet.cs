﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor(_connectionSettings));
			var converter = CreateCovariantMultiGetConverter(descriptor);
			return this.Dispatch<MultiGetDescriptor, MultiGetQueryString, MultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatch<MultiGetResponse>(p, d, converter)
			);
		}

		/// <inheritdoc />
		public Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector)
		{
			multiGetSelector.ThrowIfNull("multiGetSelector");
			var descriptor = multiGetSelector(new MultiGetDescriptor(_connectionSettings));
			var converter = CreateCovariantMultiGetConverter(descriptor);
			return this.DispatchAsync<MultiGetDescriptor, MultiGetQueryString, MultiGetResponse, IMultiGetResponse>(
				descriptor,
				(p, d) => this.RawDispatch.MgetDispatchAsync<MultiGetResponse>(p, d, converter)
			);
		}

		private JsonConverter CreateCovariantMultiGetConverter(MultiGetDescriptor descriptor)
		{
			var multiGetHitConverter = new MultiGetHitConverter(descriptor);
			return multiGetHitConverter;
		}
	}
}