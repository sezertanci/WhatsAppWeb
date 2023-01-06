namespace Common.Constants
{
    public static class RabbitMQConstants
    {
#if DEBUG
        public const string RabbitMQHost = "localhost";
#else
        public const string RabbitMQHost = "localhost";
#endif

        public const string DefaultExchangeType = "direct";

        public const string UserExchangeName = "UserExchange";
    }
}
