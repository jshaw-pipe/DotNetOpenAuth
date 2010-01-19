﻿//-----------------------------------------------------------------------
// <copyright file="AutoResponsiveRequest.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.OpenId.Provider {
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.Contracts;
	using System.Linq;
	using System.Text;
	using DotNetOpenAuth.Messaging;
	using DotNetOpenAuth.OpenId.Messages;

	/// <summary>
	/// Handles messages coming into an OpenID Provider for which the entire
	/// response message can be automatically determined without help from
	/// the hosting web site.
	/// </summary>
	internal class AutoResponsiveRequest : Request {
		/// <summary>
		/// The response message to send.
		/// </summary>
		private readonly IProtocolMessage response;

		/// <summary>
		/// Initializes a new instance of the <see cref="AutoResponsiveRequest"/> class.
		/// </summary>
		/// <param name="request">The request message.</param>
		/// <param name="response">The response that is ready for transmittal.</param>
		/// <param name="securitySettings">The security settings.</param>
		internal AutoResponsiveRequest(IDirectedProtocolMessage request, IProtocolMessage response, ProviderSecuritySettings securitySettings)
			: base(request, securitySettings) {
			Contract.Requires<ArgumentNullException>(response != null);

			this.response = response;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AutoResponsiveRequest"/> class
		/// for a response to an unrecognizable request.
		/// </summary>
		/// <param name="response">The response that is ready for transmittal.</param>
		/// <param name="securitySettings">The security settings.</param>
		internal AutoResponsiveRequest(IProtocolMessage response, ProviderSecuritySettings securitySettings)
			: base(IndirectResponseBase.GetVersion(response), securitySettings) {
			Contract.Requires<ArgumentNullException>(response != null);

			this.response = response;
		}

		/// <summary>
		/// Gets a value indicating whether the response is ready to be sent to the user agent.
		/// </summary>
		/// <remarks>
		/// This property returns false if there are properties that must be set on this
		/// request instance before the response can be sent.
		/// </remarks>
		public override bool IsResponseReady {
			get { return true; }
		}

		/// <summary>
		/// Gets the response message, once <see cref="IsResponseReady"/> is <c>true</c>.
		/// </summary>
		protected override IProtocolMessage ResponseMessage {
			get { return this.response; }
		}
	}
}
