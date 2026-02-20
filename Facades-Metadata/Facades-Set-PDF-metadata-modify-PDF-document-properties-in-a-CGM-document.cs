using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source CGM file and the resulting PDF file
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify that the CGM source file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert the CGM file to PDF using the Facade PdfProducer.
        // -----------------------------------------------------------------
        try
        {
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, pdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during CGM → PDF conversion: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Set PDF metadata (Title, Author, Subject, Keywords, Creator)
        //    using the PdfFileInfo facade.
        // -----------------------------------------------------------------
        try
        {
            var pdfInfo = new PdfFileInfo(pdfPath);
            pdfInfo.Title = "Sample PDF from CGM";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of metadata setting via Facades";
            pdfInfo.Keywords = "Aspose.Pdf, CGM, metadata, example";
            pdfInfo.Creator = "Aspose.Pdf.Facades Demo";
            pdfInfo.Save(pdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while setting metadata: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // 3. Add a text annotation with a title to the first page.
        // -----------------------------------------------------------------
        try
        {
            var pdfDoc = new Document(pdfPath);
            var page = pdfDoc.Pages[1];
            var annotRect = new Rectangle(100, 700, 300, 750);

            var textAnnot = new TextAnnotation(page, annotRect)
            {
                Title = "Important Note",
                Contents = "This PDF was generated from a CGM file and its metadata was set via Aspose.Pdf.Facades.",
                Color = Color.Yellow // Background color of the annotation (Aspose.Pdf.Color)
            };

            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            page.Annotations.Add(textAnnot);
            pdfDoc.Save(pdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while adding annotation: {ex.Message}");
            return;
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}' with updated metadata and annotation.");
    }
}
