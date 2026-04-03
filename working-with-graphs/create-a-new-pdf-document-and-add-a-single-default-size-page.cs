using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a single default‑size page (A4 is the default page size)
            doc.Pages.Add();

            // Save the PDF to disk
            doc.Save("output.pdf");
        }
    }
}