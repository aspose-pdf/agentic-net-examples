using System;
using Aspose.Pdf;

namespace InsertEmptyPageCustomSize
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: InsertEmptyPageCustomSize <width> <height>");
                return;
            }

            double width;
            double height;
            if (!double.TryParse(args[0], out width) || !double.TryParse(args[1], out height))
            {
                Console.WriteLine("Invalid width or height.");
                return;
            }

            using (Document doc = new Document())
            {
                // Add a new empty page to the document
                Page page = doc.Pages.Add();

                // Set the custom page size supplied by the user
                page.SetPageSize(width, height);

                // Save the resulting PDF
                doc.Save("output.pdf");
            }
        }
    }
}
