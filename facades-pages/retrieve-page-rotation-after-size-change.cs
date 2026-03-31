using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Rotation originalRotation = doc.Pages[1].Rotate;
            Console.WriteLine("Original rotation: " + originalRotation);

            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                editor.PageSize = Aspose.Pdf.PageSize.A4;
                editor.ApplyChanges();
            }

            Rotation afterRotation = doc.Pages[1].Rotate;
            Console.WriteLine("Rotation after size change: " + afterRotation);
            Console.WriteLine(originalRotation == afterRotation ? "Rotation unchanged." : "Rotation changed!");

            doc.Save(outputPath);
        }

        Console.WriteLine("Document saved to " + outputPath);
    }
}