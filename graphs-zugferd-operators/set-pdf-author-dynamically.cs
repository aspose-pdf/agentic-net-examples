using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output file path
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Dynamically set the Author property to the current system user name
            doc.Info.Author = Environment.UserName;

            // (Optional) set additional metadata
            doc.Info.Title = "Generated PDF";

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with Author = '{Environment.UserName}'.");
    }
}