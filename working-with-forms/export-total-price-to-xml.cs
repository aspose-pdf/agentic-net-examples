using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Calculate the total price (example implementation)
        decimal totalPrice = CalculateTotalPrice();

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with the total price
            TextFragment priceFragment = new TextFragment($"Total Price: {totalPrice:C}");
            // Set font and appearance
            priceFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            priceFragment.TextState.FontSize = 14;
            priceFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(priceFragment);

            // Save the document as XML using XmlSaveOptions
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            string xmlOutputPath = "total_price.xml";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(xmlOutputPath, xmlOptions);
            }
            else
            {
                try
                {
                    doc.Save(xmlOutputPath, xmlOptions);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The XML export was skipped to avoid a runtime crash.");
                }
            }
        }

        Console.WriteLine("Total price export attempt completed.");
    }

    // Example method to calculate total price
    static decimal CalculateTotalPrice()
    {
        // In a real scenario, replace this with actual price calculation logic
        decimal[] itemPrices = { 19.99m, 5.49m, 12.00m };
        decimal sum = 0;
        foreach (decimal price in itemPrices)
        {
            sum += price;
        }
        return sum;
    }

    // Helper to detect a missing native GDI+ library in the exception chain
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
