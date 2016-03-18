﻿using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class ListsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task CreateList_with_minimal_parameters()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == defaultSenderName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == defaultSenderAddress && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(USER_KEY, name, defaultSenderName, defaultSenderAddress);

			// Assert
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task CreateList_with_spampolicyaccepted_false()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == defaultSenderName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == defaultSenderAddress && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(USER_KEY, name, defaultSenderName, defaultSenderAddress, spamPolicyAccepted: false);

			// Assert
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task CreateList_with_spampolicyaccepted_true()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == defaultSenderName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == defaultSenderAddress && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_policy" && (string)p.Value == "accepted" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(USER_KEY, name, defaultSenderName, defaultSenderAddress, spamPolicyAccepted: true);

			// Assert
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task CreateList_with_clientid()
		{
			// Arrange
			var name = "My new list";
			var defaultSenderName = "Bob Smith";
			var defaultSenderAddress = "bobsmith@fictitiouscompany.com";
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Create/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == defaultSenderName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == defaultSenderAddress && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.CreateAsync(USER_KEY, name, defaultSenderName, defaultSenderAddress, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(listId, result);
		}

		[TestMethod]
		public async Task DeleteList_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Delete/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteAsync(USER_KEY, listId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteList_with_clientid()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Delete/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetList_with_minimal_parameters()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_includestatistics_true()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, includeStatistics: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_includestatistics_false()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, includeStatistics: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_calculateengagement_true()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, calculateEngagement: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_calculateengagement_false()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, calculateEngagement: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetList_with_clientid()
		{
			// Arrange
			var listId = 12345;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "no_details" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "with_engagement" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"name\":\"Dummy list\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}}}}", listId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(listId, result.Id);
		}

		[TestMethod]
		public async Task GetLists_with_status()
		{
			// Arrange
			var status = ListStatus.Active;

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_name()
		{
			// Arrange
			var name = "Dummy List";

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, name: name);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_sortby()
		{
			// Arrange
			var sortBy = ListsSortBy.Name;

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_sortdirection()
		{
			// Arrange
			var sortDirection = SortDirection.Ascending;

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_limit()
		{
			// Arrange
			var limit = 5;

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_offset()
		{
			// Arrange
			var offset = 25;

			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetLists_with_clientid()
		{
			// Arrange
			var jsonList1 = "{\"id\":\"123\",\"name\":\"Dummy list 1\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";
			var jsonList2 = "{\"id\":\"456\",\"name\":\"Dummy list 2\",\"status\":\"active\",\"policy\":\"declined\",\"language\":\"en_US\",\"created_on\":\"2015-03-26 22:02:45\",\"sender_name\":\"Bob Smith\",\"sender_email\":\"bobsmith@fictitiouscomapny.com\",\"forward_page\":null,\"goto_oi\":null,\"goto_di\":null,\"goto_oo\":null,\"b_ac_limit\":\"3\",\"b_cr_limit\":\"3\",\"b_df_limit\":\"3\",\"b_fm_limit\":\"3\",\"b_hb_limit\":\"0\",\"b_mb_limit\":\"3\",\"b_sb_limit\":\"3\",\"b_tr_limit\":\"3\",\"di_trig_cnt\":\"0\",\"oi_trig_cnt\":\"0\",\"oo_trig_cnt\":\"0\",\"oi_url\":\"http://link.fictitiouscompany.com/oi/1/2b494468e2a377f39751ff716103fd49\",\"subscribe_url\":\"http://link.fictitiouscompany.com/s/1/2b494468e2a377f39751ff716103fd49\",\"oo_url\":\"http://link.fictitiouscompany.com/oo/1/2b494468e2a377f39751ff716103fd49\",\"webhook\":null,\"engagement\":null,\"pending\":\"0\",\"active\":\"0\",\"bounced\":\"0\",\"invalid\":\"0\",\"unsubscribed\":\"0\",\"spam\":\"0\",\"deleted\":\"0\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"lists\":[{0},{1}]}}}}", jsonList1, jsonList2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetListsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(123, result.ToArray()[0].Id);
			Assert.AreEqual(456, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListsCount_with_status()
		{
			// Arrange
			var status = ListStatus.Active;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetCountAsync(USER_KEY, status: status);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListsCount_with_name()
		{
			// Arrange
			var name = "Dummy List";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetCountAsync(USER_KEY, name: name);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListsCount_with_clientid()
		{
			// Arrange
			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetList/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateList_name()
		{
			// Arrange
			var listId = 123;
			var name = "My list";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "name" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, name: name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_language()
		{
			// Arrange
			var listId = 123;
			var language = "fr-FR";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "language" && (string)p.Value == language && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, language: language);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_spampolicy_accepted()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_policy" && (string)p.Value == "accepted" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, spamPolicyAccepted: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_spampolicy_declined()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_policy" && (string)p.Value == "declined" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, spamPolicyAccepted: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_status()
		{
			// Arrange
			var listId = 123;
			var status = ListStatus.Archived;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, status: status);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_sendername()
		{
			// Arrange
			var listId = 123;
			var senderName = "Bob Smith";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_name" && (string)p.Value == senderName && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, senderName: senderName);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_senderemail()
		{
			// Arrange
			var listId = 123;
			var senderEmail = "bobsmith@fictitiouscompany.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sender_email" && (string)p.Value == senderEmail && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, senderEmail: senderEmail);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_gotooi()
		{
			// Arrange
			var listId = 123;
			var goto_oi = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "goto_oi" && (string)p.Value == goto_oi && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, goto_oi: goto_oi);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_gotodi()
		{
			// Arrange
			var listId = 123;
			var goto_di = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "goto_di" && (string)p.Value == goto_di && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, goto_di: goto_di);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_gotooo()
		{
			// Arrange
			var listId = 123;
			var goto_oo = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "goto_oo" && (string)p.Value == goto_oo && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, goto_oo: goto_oo);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_webhook()
		{
			// Arrange
			var listId = 123;
			var webhook = "???";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "webhook" && (string)p.Value == webhook && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, webhook: webhook);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateList_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SetInfo/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task AddListField_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;
			var name = "My field";
			var fieldType = FieldType.Text;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/EditStructure/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == fieldType.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "add" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddFieldAsync(USER_KEY, listId, name, fieldType);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task AddListField_with_clientid()
		{
			// Arrange
			var listId = 123;
			var name = "My field";
			var fieldType = FieldType.Integer;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/EditStructure/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "type" && (string)p.Value == fieldType.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "add" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddFieldAsync(USER_KEY, listId, name, fieldType, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListField_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;
			var name = "My field";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/EditStructure/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "delete" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteFieldAsync(USER_KEY, listId, name);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListField_with_clientid()
		{
			// Arrange
			var listId = 123;
			var name = "My field";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/EditStructure/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "field" && (string)p.Value == name && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == "delete" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteFieldAsync(USER_KEY, listId, name, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetListFields_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetFields/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetFieldsAsync(USER_KEY, listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task GetListFields_with_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetFields/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"id\":\"integer\",\"email\":\"text\",\"registered\":\"timestamp\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetFieldsAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.Count());
		}

		[TestMethod]
		public async Task GetListFields_returns_null()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetFields/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":null}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetFieldsAsync(USER_KEY, listId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count());
		}

		[TestMethod]
		public async Task AddTestEmail_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/AddTestEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddTestEmailAsync(USER_KEY, listId, email);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task AddTestEmail_with_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/AddTestEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.AddTestEmailAsync(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteTestEmail_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/DeleteTestEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteTestEmailAsync(USER_KEY, listId, email);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteTestEmail_with_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/DeleteTestEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteTestEmailAsync(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTestEmails_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetTestEmails/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 2 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetTestEmailsAsync(USER_KEY, listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(testEmail1, result.ToArray()[0]);
			Assert.AreEqual(testEmail2, result.ToArray()[1]);
		}

		[TestMethod]
		public async Task GetTestEmails_with_clientid()
		{
			// Arrange
			var listId = 123;

			var testEmail1 = "aaa@aaa.com";
			var testEmail2 = "bbb@bbb.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetTestEmails/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"testemails\":[\"{0}\",\"{1}\"]}}}}", testEmail1, testEmail2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetTestEmailsAsync(USER_KEY, listId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(testEmail1, result.ToArray()[0]);
			Assert.AreEqual(testEmail2, result.ToArray()[1]);
		}

		[TestMethod]
		public async Task Subscribe_with_autoresponders_true()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, autoResponders: true);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_autoresponders_false()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, autoResponders: false);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_triggers_true()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, autoResponders: true);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_triggers_false()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, triggers: false);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_customfields()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var firstName = "Bob";
			var lastName = "Smith";

			var customFields = new[]
			{
				new KeyValuePair<string, object>("firstname", firstName),
				new KeyValuePair<string, object>("lastname", lastName),
				new KeyValuePair<string, object>("birthday", new DateTime(1973, 1, 1))
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[firstname]" && (string)p.Value == firstName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[lastname]" && (string)p.Value == lastName && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[birthday]" && (string)p.Value == "1973-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, customFields: customFields);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Subscribe_with_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "aaa@aaa.com";
			var subscriberId = 777;
			var firstName = "Bob";
			var lastName = "Smith";

			var customFields = new[]
			{
				new KeyValuePair<string, object>("firstname", firstName),
				new KeyValuePair<string, object>("lastname", lastName),
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/SubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{0}}}", subscriberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.SubscribeAsync(USER_KEY, listId, email, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(subscriberId, result);
		}

		[TestMethod]
		public async Task Import_with_autoresponders_true()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Import/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, autoResponders: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_autoresponders_false()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Import/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, autoResponders: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_triggers_true()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Import/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, triggers: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_triggers_false()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Import/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 7 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, triggers: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_customfields()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com", CustomFields = new Dictionary<string, object>() { { "firstname", "Bob" }, { "lastname", "Smith" }, { "age", 41 }, { "birthday", new DateTime(1973, 1, 1) } } },
				new ListMember() { Email = "bbb@bbb.com", CustomFields = new Dictionary<string, object>() { { "firstname", "Jane" }, { "lastname", "Doe" }, { "age", 50 }, { "birthday", new DateTime(1964, 1, 1) } } }
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Import/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 15 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][firstname]" && (string)p.Value == "Bob" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][lastname]" && (string)p.Value == "Smith" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][age]" && (int)p.Value == 41 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][birthday]" && (string)p.Value == "1973-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][firstname]" && (string)p.Value == "Jane" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][lastname]" && (string)p.Value == "Doe" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][age]" && (int)p.Value == 50 && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][birthday]" && (string)p.Value == "1964-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, autoResponders: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_with_clientid()
		{
			// Arrange
			var listId = 123;
			var listMembers = new[]
			{
				new ListMember() { Email = "aaa@aaa.com" },
				new ListMember() { Email = "bbb@bbb.com" },
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Import/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 8 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[0][email]" && (string)p.Value == "aaa@aaa.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record[1][email]" && (string)p.Value == "bbb@bbb.com" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Import_zero_subscribers()
		{
			// Arrange
			var listId = 123;
			var listMembers = (ListMember[])null;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Import/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "import_to" && (string)p.Value == "active" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "autoresponders" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "triggers" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":[{\"email\":\"aa@aa.com\",\"id\":\"1\"},{\"email\":\"bbb@bbb.com\",\"id\":\"2\"}]}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.ImportAsync(USER_KEY, listId, listMembers);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task Unsubscribe_by_email()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/UnsubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, email, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task Unsubscribe_by_email_with_clientid()
		{
			// Arrange
			var listId = 123;
			var email = "test@test.com";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/UnsubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "email" && (string)p.Value == email && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, email, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task Unsubscribe_by_memberid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/UnsubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, memberId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task Unsubscribe_by_memberid_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/UnsubscribeEmail/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UnsubscribeAsync(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListMember()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/DeleteRecord/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteMemberAsync(USER_KEY, listId, memberId, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteListMember_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/DeleteRecord/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.DeleteMemberAsync(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetListMember()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetRecord/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMemberAsync(USER_KEY, listId, memberId, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(memberId, result.Id);
		}

		[TestMethod]
		public async Task GetListMember_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 555;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetRecord/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}}}}", memberId)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMemberAsync(USER_KEY, listId, memberId, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(memberId, result.Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_status()
		{
			// Arrange
			var listId = 123;
			var status = ListMemberStatus.Active;

			var jsonMember1 = string.Format("{{\"id\":\"1\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);
			var jsonMember2 = string.Format("{{\"id\":\"2\",\"status\":\"{0}\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}}", status);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_query()
		{
			// Arrange
			var listId = 123;
			var query = "(... this is a bogus query ...)";

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "query" && (string)p.Value == query && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, query: query);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_sortby()
		{
			// Arrange
			var listId = 123;
			var sortBy = ListMembersSortBy.EmailAddress;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "sort_by" && (string)p.Value == sortBy.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, sortBy: sortBy);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_sortdirection()
		{
			// Arrange
			var listId = 123;
			var sortDirection = SortDirection.Ascending;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "direction" && (string)p.Value == sortDirection.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, sortDirection: sortDirection);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_limit()
		{
			// Arrange
			var listId = 123;
			var limit = 5;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_offset()
		{
			// Arrange
			var listId = 123;
			var offset = 25;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembers_with_clientid()
		{
			// Arrange
			var listId = 123;

			var jsonMember1 = "{\"id\":\"1\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"aa@aa.com\",\"registered\":\"2015-04-01 15:08:22\"}";
			var jsonMember2 = "{\"id\":\"2\",\"status\":\"active\",\"bounce_type\":\"none\",\"bounce_count\":\"0\",\"email\":\"bb@bb.com\",\"registered\":\"2015-04-01 15:08:22\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"records\":[{0},{1}]}}}}", jsonMember1, jsonMember2)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result.ToArray()[0].Id);
			Assert.AreEqual(2, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetListMembersCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersCountAsync(USER_KEY, listId);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListMembersCount_with_status()
		{
			// Arrange
			var listId = 123;
			var status = ListMemberStatus.Active;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "status" && (string)p.Value == status.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersCountAsync(USER_KEY, listId, status: status);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListMembersCount_with_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/Show/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetMembersCountAsync(USER_KEY, listId, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateMember_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;
			var memberId = 456;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/UpdateRecord/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 3 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateMemberAsync(USER_KEY, listId, memberId);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMember_with_customfields()
		{
			// Arrange
			var listId = 123;
			var memberId = 456;
			var customFields = new[]
			{
				new KeyValuePair<string, object>("fullname", "Bob Smith"),
				new KeyValuePair<string, object>("birthday", new DateTime(1973, 1, 1))
			};

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/UpdateRecord/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[fullname]" && (string)p.Value == "Bob Smith" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "data[birthday]" && (string)p.Value == "1973-01-01 00:00:00" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateMemberAsync(USER_KEY, listId, memberId, customFields: customFields);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateMember_with_clientid()
		{
			// Arrange
			var listId = 123;
			var memberId = 456;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/UpdateRecord/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 4 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "record_id" && (long)p.Value == memberId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":\"true\"}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.UpdateMemberAsync(USER_KEY, listId, memberId, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetListLogs_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_logtype()
		{
			// Arrange
			var listId = 123;
			var logType = LogType.Subscribe;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == logType.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0}]}}}}", subscribeLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, logType: logType);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_startdate()
		{
			// Arrange
			var listId = 123;
			var start = new DateTime(2015, 1, 1);

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == start.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_enddate()
		{
			// Arrange
			var listId = 123;
			var end = new DateTime(2015, 12, 31);

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == end.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_limit()
		{
			// Arrange
			var listId = 123;
			var limit = 5;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "limit" && (int)p.Value == limit && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_offset()
		{
			// Arrange
			var listId = 123;
			var offset = 25;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "offset" && (int)p.Value == offset && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_clientid()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_uniques_true()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: true, totals: false);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogs_with_totals_true()
		{
			// Arrange
			var listId = 123;

			var subscribeLog = "{\"log_id\":\"70\",\"id\":\"70\",\"record_id\":\"70\",\"email\":\"aaa@aaa.com\",\"action\":\"subscribe\",\"total\":\"1\",\"time\":\"2015-03-06 16:25:40\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"insert\",\"l_registered\":\"2015-03-06 16:25:40\"}";
			var sentLog = "{\"log_id\":\"249\",\"id\":\"124\",\"record_id\":\"124\",\"email\":\"aaa@aaa.com\",\"action\":\"in_queue\",\"total\":\"1\",\"time\":\"2015-03-06 16:27:00\",\"user_agent\":null,\"ip\":null,\"host\":null,\"extra\":\"mailing_id = 12345\",\"l_registered\":\"2015-03-06 16:25:44\"}";

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = string.Format("{{\"status\":\"success\",\"data\":{{\"logs\":[{0},{1}]}}}}", subscribeLog, sentLog)
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsAsync(USER_KEY, listId, uniques: false, totals: true);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetListLogsCount_with_minimal_parameters()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_logtype()
		{
			// Arrange
			var listId = 123;
			var logType = LogType.Click;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "action" && (string)p.Value == logType.GetEnumMemberValue() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, logType: logType);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_startdate()
		{
			// Arrange
			var listId = 123;
			var start = new DateTime(2015, 1, 1);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "start_time" && (string)p.Value == start.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: false, start: start);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_enddate()
		{
			// Arrange
			var listId = 123;
			var end = new DateTime(2015, 12, 31);

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "end_time" && (string)p.Value == end.ToCakeMailString() && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: false, end: end);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_clientid()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 6 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "client_id" && (long)p.Value == CLIENT_ID && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: false, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_uniques_true()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: true, totals: false);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetListLogsCount_with_totals_true()
		{
			// Arrange
			var listId = 123;

			var mockRestClient = new Mock<IRestClient>(MockBehavior.Strict);
			mockRestClient.Setup(m => m.BaseUrl).Returns(new Uri("http://localhost"));
			mockRestClient.Setup(m => m.ExecuteTaskAsync(It.Is<IRestRequest>(r =>
				r.Method == Method.POST &&
				r.Resource == "/List/GetLog/" &&
				r.Parameters.Count(p => p.Name == "apikey" && (string)p.Value == API_KEY && p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.HttpHeader) == 1 &&
				r.Parameters.Count(p => p.Type == ParameterType.GetOrPost) == 5 &&
				r.Parameters.Count(p => p.Name == "user_key" && (string)p.Value == USER_KEY && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "list_id" && (long)p.Value == listId && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "uniques" && (string)p.Value == "false" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "totals" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1 &&
				r.Parameters.Count(p => p.Name == "count" && (string)p.Value == "true" && p.Type == ParameterType.GetOrPost) == 1
			))).ReturnsAsync(new RestResponse()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "json",
				Content = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}"
			});

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Lists.GetLogsCountAsync(USER_KEY, listId, uniques: false, totals: true);

			// Assert
			Assert.AreEqual(2, result);
		}
	}
}
