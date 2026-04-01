using System;
using Aspose.Pdf;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with two pages of different sizes
            using (Document doc = new Document())
            {
                // First page uses default size (A4)
                Page page1 = doc.Pages.Add();

                // Second page with a custom size (500 x 700 points)
                Page page2 = doc.Pages.Add();
                page2.SetPageSize(500.0, 700.0);

                // Save the sample PDF
                doc.Save("sample.pdf");
            }

            // Load the PDF and log each page's dimensions
            using (Document doc = new Document("sample.pdf"))
            {
                int pageCount = doc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = doc.Pages[i];
                    double width = page.Rect.Width;
                    double height = page.Rect.Height;
                    Console.WriteLine($"Page {i}: Width = {width}, Height = {height}");
                }
            }
        }
    }
}