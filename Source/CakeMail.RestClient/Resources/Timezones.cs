﻿using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage timezones
	/// </summary>
	/// <seealso cref="CakeMail.RestClient.Resources.ITimezones" />
	public class Timezones : ITimezones
	{
		#region Fields

		private readonly IClient _client;

		#endregion

		#region Constructor

		internal Timezones(IClient client)
		{
			_client = client;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Retrieve the list of all timezones known to the CakeMail system
		/// </summary>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of all <see cref="Timezone">timezones</see>.</returns>
		public Task<Timezone[]> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return _client
				.PostAsync("Client/GetTimezones")
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Timezone[]>("timezones");
		}

		#endregion
	}
}
