public class TradingEngine
{
    private readonly Dictionary<string, decimal> _lastPrices = new();
    private readonly List<Action<TradeExecuted>> _tradeSubscribers = new();
    private int _nextOrderId = 1;
    private readonly object _lock = new();

    public void ProcessOrder(Order order)
    {
        int orderId;
        lock (_lock)
        {
            orderId = _nextOrderId++;
        }
        order.OrderId = orderId;

        if (!_lastPrices.TryGetValue(order.Symbol, out var currentPrice))
        {
            throw new InvalidOperationException($"No price data for {order.Symbol}");
        }

        decimal executionPrice = currentPrice;
        if (order.Type == OrderType.Limit)
        {
            if ((order.Side == OrderSide.Buy && order.LimitPrice < currentPrice) ||
                (order.Side == OrderSide.Sell && order.LimitPrice > currentPrice))
            {
                Console.WriteLine($"Order {order.OrderId} not executed: limit price not met");
                return;
            }
            executionPrice = order.LimitPrice;
        }

        var trade = new TradeExecuted
        {
            OrderId = order.OrderId,
            Symbol = order.Symbol,
            Quantity = order.Quantity,
            Price = executionPrice,
            Timestamp = DateTime.UtcNow
        };

        foreach (var subscriber in _tradeSubscribers)
        {
            subscriber(trade);
        }
    }

    public void UpdatePrice(string symbol, decimal price)
    {
        _lastPrices[symbol] = price;
    }

    public void SubscribeToTrades(Action<TradeExecuted> callback)
    {
        if (callback == null) throw new ArgumentNullException(nameof(callback));
        _tradeSubscribers.Add(callback);
    }

    // Missing method: what important functionality should be here?
}

public class Order
{
    public int OrderId { get; set; }
    public string Symbol { get; set; }
    public decimal Quantity { get; set; }
    public OrderSide Side { get; set; }
    public OrderType Type { get; set; }
    public decimal? LimitPrice { get; set; }
}

public enum OrderSide { Buy, Sell }
public enum OrderType { Market, Limit }

public class TradeExecuted
{
    public int OrderId { get; set; }
    public string Symbol { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime Timestamp { get; set; }
}