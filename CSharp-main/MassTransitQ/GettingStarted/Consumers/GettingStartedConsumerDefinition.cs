namespace Company.Consumers;
public class GettingStartedConsumerDefinition :
    ConsumerDefinition<GettingStartedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GettingStartedConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}