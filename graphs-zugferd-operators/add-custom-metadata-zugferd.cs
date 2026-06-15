using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the PDF and add custom metadata entries
        using (Document doc = new Document("input.pdf"))
        {
            // Add custom metadata (e.g., project code and department)
            doc.Info.Add("ProjectCode", "PRJ-001");
            doc.Info.Add("Department", "Finance");

            // Save the updated PDF
            doc.Save("output.pdf");
        }
    }
}