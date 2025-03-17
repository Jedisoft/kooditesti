public class OrderMatcher
{
    private readonly Queue<Order> _buyOrders = new();
    private readonly Queue<Order> _sellOrders = new();
    private readonly TradingEngine _tradingEngine;
    private readonly SemaphoreSlim _matchingSemaphore = new(1, 1);

    public OrderMatcher(TradingEngine tradingEngine)
    {
        _tradingEngine = tradingEngine;
    }

    public void AddOrder(Order order)
    {
        if (order.Side == OrderSide.Buy)
        {
            _buyOrders.Enqueue(order);
        }
        else
        {
            _sellOrders.Enqueue(order);
        }

        // TASK: Implement a non-blocking way to trigger the matching process
        // The current implementation below is problematic - fix it
        Task.Run(() => MatchOrdersAsync().Wait());
    }

    private async Task MatchOrdersAsync()
    {
        // This implementation is incomplete and has concurrency issues
        // TASK: Implement a proper matching algorithm that:
        // 1. Doesn't block other operations
        // 2. Avoids race conditions
        // 3. Matches orders based on price-time priority

        await _matchingSemaphore.WaitAsync();
        try
        {
            // Very naive implementation - replace with something better
            if (_buyOrders.TryPeek(out var buyOrder) && _sellOrders.TryPeek(out var sellOrder))
            {
                if (buyOrder.Symbol == sellOrder.Symbol)
                {
                    _buyOrders.TryDequeue(out _);
                    _sellOrders.TryDequeue(out _);

                    _tradingEngine.ProcessOrder(buyOrder);
                    _tradingEngine.ProcessOrder(sellOrder);
                }
            }
        }
        finally
        {
            _matchingSemaphore.Release();
        }
    }

    // TASK: Implement additional methods needed for proper order matching
}
