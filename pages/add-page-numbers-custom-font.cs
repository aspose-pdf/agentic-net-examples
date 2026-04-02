using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddPageNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document with three pages
            using (Document document = new Document())
            {
                // Add three blank pages
                int pageCount = 3;
                for (int i = 0; i < pageCount; i++)
                {
                    document.Pages.Add();
                }

                // Create a page number stamp with custom font and size
                PageNumberStamp pageNumberStamp = new PageNumberStamp();
                // Font property expects a Font object, obtain it from FontRepository
                pageNumberStamp.TextState.Font = FontRepository.FindFont("Arial");
                pageNumberStamp.TextState.FontSize = 14;
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
                pageNumberStamp.BottomMargin = 10;

                // Apply the stamp to each page
                foreach (Page page in document.Pages)
                {
                    page.AddStamp(pageNumberStamp);
                }

                // Save the resulting PDF
                document.Save("output.pdf");
            }
        }
    }
}
