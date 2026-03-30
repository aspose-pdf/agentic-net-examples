using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "filled.pdf";

        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF form from a file path
            autoFiller.BindPdf(inputPath);

            // Save the (still empty) form to a new file
            autoFiller.Save(outputPath);
        }

        Console.WriteLine("Form bound and saved to '" + outputPath + "'.");
    }
}