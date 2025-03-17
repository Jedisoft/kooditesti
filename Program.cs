// sunnittele sovellus- ja järjestelmäarkkitehtuuri treidausohjelmiston kokonaisuudelle (teksti tai kuva)
// viimeistele treidausohjelmiston treidauskirjaston sekä sen stressitestin puuttuvat osiot ja paranna jo olemassa olevia osioita
// huom. ei tarvitse toteuttaa itse treidausohjelmistoa. sen rajapintoja jne.

// voit muokata mitä tahaansa kokonaisuuden osaa jos näet sen parantavan lopputulosta
// lopputuloksen pitää kääntyä, mutta kaikkea ei tarvitse tehdä valmiiksi vaan riittää, että kuvaat kirjallisesti mitä olisit tehnyt

// lopputulokselta odotetaan kaikkea sitä mitä ohjelmistoprojektin tuotannon käyttöönotto vaatii.
// arvioinnissa tarkastellaan kokonaisuuden laatua, ei määrää
// kirjaa ylös tehtävään käytetty aika

class Program
{
    static async Task Main(string[] args)
    {
        var tradingEngine = new TradingEngine();
        var orderMatcher = new OrderMatcher(tradingEngine);
        var marketData = new MarketData(tradingEngine);

        // TASK: Implement a stress test that:
        // 1. Generates high volume of market data updates
        // 2. Submits orders at varying rates
        // 3. Measures and reports system throughput and latency
        // 4. Detects and reports any errors or inconsistencies

        string[] symbols = { "AAPL", "MSFT", "GOOGL", "AMZN", "META" };

        // TASK: Create and start your stress test here

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}