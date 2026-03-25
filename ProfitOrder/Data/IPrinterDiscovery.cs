using System;
using System.Collections.Generic;
using System.Text;
using Zebra.Sdk.Printer.Discovery;

namespace ProfitOrder.Data
{
    public interface IPrinterDiscovery
    {
        //void FindBluetoothPrinters(IDiscoveryHandler handler);
        //void FindUSBPrinters(IDiscoveryHandler handler);
        //void RequestUSBPermission(IDiscoveredPrinterUsb printer);
        void CancelDiscovery();
    }
}
