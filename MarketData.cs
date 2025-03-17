public class MarketData
{
    private readonly TradingEngine _tradingEngine;
    private readonly int _maxQueueSize = 10000;

    // TASK: Implement a bounded buffer for market data with backpressure handling

    public MarketData(TradingEngine tradingEngine)
    {
        _tradingEngine = tradingEngine;
    }

    public void ProcessPriceUpdate(string symbol, decimal price)
    {
        // Direct processing - no buffering or throttling
        // This will cause problems under high load
        _tradingEngine.UpdatePrice(symbol, price);
    }

    // TASK: Implement methods to start and stop the market data processing

    // TASK: Implement a way to handle backpressure when updates come too quickly
}

