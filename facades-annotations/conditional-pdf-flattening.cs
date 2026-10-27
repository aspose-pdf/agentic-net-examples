using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPath))
            {
                // Detect digital signature fields by inspecting Form.Fields collection
                bool hasSignature = false;
                if (pdfDoc.Form != null && pdfDoc.Form.Fields != null)
                {
                    foreach (var field in pdfDoc.Form.Fields)
                    {
                        if (field is SignatureField)
                        {
                            hasSignature = true;
                            break;
                        }
                    }
                }

                if (hasSignature)
                {
                    Console.WriteLine("PDF contains digital signatures. Skipping flattening to preserve signature validity.");
                    // Copy the original file to the output location
                    File.Copy(inputPath, outputPath, overwrite: true);
                    Console.WriteLine($"Original PDF copied to '{outputPath}'.");
                    return;
                }

                // No signatures – flatten form fields
                pdfDoc.Form?.Flatten();

                // Flatten annotations using PdfAnnotationEditor (correct API)
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfDoc);
                    editor.FlatteningAnnotations();
                }

                pdfDoc.Save(outputPath);
                Console.WriteLine($"Flattening completed. Output saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
