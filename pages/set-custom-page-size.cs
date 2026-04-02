using System;
using Aspose.Pdf;

namespace AsposePdfExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document document = new Document())
            {
                // Add a blank page
                Page page = document.Pages.Add();

                // Set custom size for the first page (500 x 700 points)
                page.SetPageSize(500.0, 700.0);

                // Save the document
                document.Save("output.pdf");
            }
        }
    }
}
