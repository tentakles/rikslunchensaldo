using System;
using RiksLunchenSaldo;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Input card number.");
			var cardnumber = Console.ReadLine();

			try {
				var saldo = RiksLunchenSaldoProvider.GetSaldo(cardnumber);
				PresentBalance(saldo);
			} catch (Exception e) {
				Console.WriteLine("Fel uppstod. Kunde inte hämta saldo.");
			}

			Console.WriteLine("Programkörning avslutad. tryck valfri tangent för att avsluta.");
			Console.ReadKey();
		}

		private static void PresentBalance(RiksLunchenSaldoData saldo)
		{
			Console.WriteLine("Hämtade saldoinformation:");

			Console.WriteLine("Saldo: " + saldo.balance);
			Console.WriteLine("Kortnummer: " + saldo.cardNo);
			Console.WriteLine("Senaste påfyllningsdatum: " + saldo.lastTopupDate.ToShortDateString());
			Console.WriteLine("Meddelande: " + saldo.message);
			Console.WriteLine("Transfered (?): " + saldo.transfered);
			Console.WriteLine("Giltigt: " + saldo.valid);
		}
	}
}
