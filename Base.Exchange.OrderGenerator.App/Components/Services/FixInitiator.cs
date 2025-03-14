using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix.Transport;

namespace Base.Exchange.OrderGenerator.App.Components.Services
{
    public class FixInitiator : MessageCracker, IApplication
    {
        private readonly SocketInitiator _initiator;
        private SessionID? _sessionID;

        public FixInitiator()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string configPath = Path.Combine(projectRoot, "Config", "initiator.cfg");

            var settings = new SessionSettings(configPath);
            var storeFactory = new FileStoreFactory(settings);
            var logFactory = new FileLogFactory(settings);
            var application = this;
            
            _initiator = new SocketInitiator(application, storeFactory, settings, logFactory);
        }

        public void StartSession()
        {
            _initiator.Start();
            
            Console.WriteLine("FIX Session Started.");
        }

        public void StopSession()
        {
            _initiator.Stop();
            Console.WriteLine("FIX Session Stopped.");
        }

        public void FromAdmin(QuickFix.Message message, SessionID sessionID) { }
        public void OnCreate(SessionID sessionID) { }
        public void OnLogout(SessionID sessionID) { }
        public void OnLogon(SessionID sessionID) 
        {
            _sessionID = sessionID;
        }
        public SessionID GetSessionID() => _sessionID;
        public void ToAdmin(QuickFix.Message message, SessionID sessionID) { }
        public void ToApp(QuickFix.Message message, SessionID sessionID) { }
        public void FromApp(QuickFix.Message message, SessionID sessionID) 
        {
            Console.WriteLine(message);
        }        

        public void SendNewOrder(string symbol, string side, int quantity, decimal price)
        {
            try
            {
                var order = new NewOrderSingle(
                    new ClOrdID(Guid.NewGuid().ToString()),
                    new Symbol(symbol),                    
                    new Side(side == "BUY" ? Side.BUY : Side.SELL),
                    new TransactTime(DateTime.UtcNow),
                    new OrdType(OrdType.MARKET)
                );

                order.Set(new Price(price));
                order.Set(new OrderQty(quantity));

                Session.SendToTarget(order, _sessionID);
            }
            catch (Exception e)
            {

                Console.WriteLine("Error on create order:", e.Message);
            }
        }
    }
}
