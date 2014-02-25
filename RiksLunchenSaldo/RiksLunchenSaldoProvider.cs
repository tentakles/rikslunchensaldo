using System;
using System.Globalization;
using System.Net;

namespace RiksLunchenSaldo
{
	public class RiksLunchenSaldoProvider
	{
		const string Url1 = "https://www.rikslunchen.se/riks-cp/check_balance.html";
		const string Url2 = "https://www.rikslunchen.se/riks-cp/dwr/call/plaincall/__System.pageLoaded.dwr";
		const string Url3 = "https://www.rikslunchen.se/riks-cp/dwr/call/plaincall/cardUtil.getCardData.dwr";

		const string Finddatastr = "{";
		const string Finddatastr2 = "}";
		const string Findsessionstr = "handleNewScriptSession(\"";
		const int Keylen = 32;

		public static RiksLunchenSaldoData GetSaldo(string cardNumber)
		{
			var request = (HttpWebRequest)WebRequest.Create(Url1);
			request.CookieContainer = new CookieContainer();
			var response1 = (HttpWebResponse)request.GetResponse();
			var cookievalue = response1.Cookies[0].Value;

			string scriptKey;

			using (var client = new WebClient()) {

				string mydata = "callCount" + "=" + "1" + Environment.NewLine;
				mydata += "windowName" + "=" + "" + Environment.NewLine;
				mydata += "c0-scriptName" + "=" + "__System" + Environment.NewLine;
				mydata += "c0-methodName" + "=" + "pageLoaded" + Environment.NewLine;
				mydata += "c0-id" + "=" + "0" + Environment.NewLine;
				mydata += "batchId" + "=" + "0" + Environment.NewLine;
				mydata += "page" + "=" + "%2Friks-cp%2Fcheck_balance.html" + Environment.NewLine;
				mydata += "httpSessionId" + "=" + cookievalue + Environment.NewLine;
				mydata += "scriptSessionId" + "=" + "" + Environment.NewLine;

				var result = client.UploadString(Url2, mydata);
				scriptKey = GetScriptKeyFromResponse(result);
			}

			using (var client2 = new CookieAwareWebClient()) {

				client2.CookieContainer = request.CookieContainer;

				string mydata2 = "callCount" + "=" + "1" + Environment.NewLine;
				mydata2 += "windowName" + "=" + "" + Environment.NewLine;
				mydata2 += "c0-scriptName" + "=" + "cardUtil" + Environment.NewLine;
				mydata2 += "c0-methodName" + "=" + "getCardData" + Environment.NewLine;
				mydata2 += "c0-id" + "=" + "0" + Environment.NewLine;
				mydata2 += "c0-param0" + "=string:" + cardNumber + Environment.NewLine;
				mydata2 += "c0-param1" + "=string:" + cookievalue + Environment.NewLine;
				mydata2 += "batchId" + "=" + "1" + Environment.NewLine;
				mydata2 += "page" + "=" + "%2Friks-cp%2Fcheck_balance.html" + Environment.NewLine;
				mydata2 += "httpSessionId" + "=" + cookievalue + Environment.NewLine;
				mydata2 += "scriptSessionId" + "=" + scriptKey + Environment.NewLine;

				var result2 = client2.UploadString(Url3, mydata2);


				RiksLunchenSaldoData result = ConvertResultToData(result2);
				return result;
			}
		}

		private static RiksLunchenSaldoData ConvertResultToData(string result2)
		{
			var result = new RiksLunchenSaldoData();

			var pos1 = result2.LastIndexOf(Finddatastr, StringComparison.Ordinal) + 1;
			var pos2 = result2.LastIndexOf(Finddatastr2, StringComparison.Ordinal);

			var poslength = pos2 - pos1;
			string result3 = result2.Substring(pos1, poslength);
			result3 = result3.Replace("\"", "");
			var resultrows = result3.Split(',');

			result.balance = double.Parse(GetVal(0, resultrows), CultureInfo.InvariantCulture);
			result.cardNo = GetVal(1, resultrows);
			result.lastTopupDate = DateTime.Parse(GetVal(2, resultrows), CultureInfo.InvariantCulture);
			result.message = GetVal(3, resultrows);
			result.transfered = int.Parse(GetVal(4, resultrows));
			result.valid = bool.Parse(GetVal(5, resultrows));

			return result;
		}

		private static string GetVal(int p, string[] resultrows)
		{
			var result = resultrows[p].Split(':')[1];
			return result;
		}

		private static string GetScriptKeyFromResponse(string result)
		{

			var pos1 = result.IndexOf(Findsessionstr, StringComparison.Ordinal);

			var scriptkey = result.Substring(pos1 + Findsessionstr.Length, Keylen);
			return scriptkey;
		}

		public static RiksLunchenSaldoData GetDummy(string cardNumber)
		{
			var result = new RiksLunchenSaldoData {
				balance = 112,
				cardNo = cardNumber,
				lastTopupDate = DateTime.Now,
				message = "",
				transfered = 0,
				valid = false
			};
			return result;
		}
	}
}
