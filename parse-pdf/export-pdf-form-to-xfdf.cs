using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "output.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Form facade to export XFDF data
        using (Form formFacade = new Form())
        {
            formFacade.BindPdf(inputPdf);

            // Create a FileStream to write the XFDF data
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export form annotations to XFDF using the facade overload that accepts a Stream
                formFacade.ExportXfdf(xfdfStream);
                // Stream will be closed automatically by the using block
            }
        }

        Console.WriteLine($"XFDF data exported to '{xfdfPath}'.");
    }
}
