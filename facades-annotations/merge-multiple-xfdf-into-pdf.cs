using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MergeXfdfIntoPdf
{
    static void Main()
    {
        // Input PDF and XFDF files
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_merged.pdf";
        string[] xfdfFiles = { "file1.xfdf", "file2.xfdf", "file3.xfdf" };

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        foreach (var xfdf in xfdfFiles)
        {
            if (!File.Exists(xfdf))
            {
                Console.Error.WriteLine($"XFDF not found: {xfdf}");
                return;
            }
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Merge XFDF files into a single XDocument
            XDocument mergedXfdf = null;
            foreach (var xfdfPath in xfdfFiles)
            {
                using (FileStream fs = File.OpenRead(xfdfPath))
                {
                    XDocument current = XDocument.Load(fs);
                    if (mergedXfdf == null)
                    {
                        // First file becomes the base
                        mergedXfdf = new XDocument(current);
                    }
                    else
                    {
                        // Append child nodes (fields, annots, etc.) from subsequent files
                        foreach (var element in current.Root.Elements())
                        {
                            mergedXfdf.Root.Add(new XElement(element));
                        }
                    }
                }
            }

            if (mergedXfdf == null)
            {
                Console.Error.WriteLine("No XFDF content to merge.");
                return;
            }

            // Write merged XFDF to a memory stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                mergedXfdf.Save(xfdfStream);
                xfdfStream.Position = 0; // Reset for reading

                // Import form fields using Form facade
                Form formFacade = new Form(pdfDoc);
                formFacade.ImportXfdf(xfdfStream);
                // Reset stream position for the next import
                xfdfStream.Position = 0;

                // Import annotations using PdfAnnotationEditor facade
                PdfAnnotationEditor annotEditor = new PdfAnnotationEditor(pdfDoc);
                annotEditor.ImportAnnotationsFromXfdf(xfdfStream);
            }

            // Save the updated PDF (lifecycle rule)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged XFDF imported and PDF saved to '{outputPdfPath}'.");
    }
}