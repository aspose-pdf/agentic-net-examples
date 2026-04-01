using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputImage = "witness_signature.png";
        const string targetFieldName = "WitnessSignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                bool imageExtracted = false;
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    foreach (Annotation annotation in page.Annotations)
                    {
                        if (annotation is SignatureField)
                        {
                            SignatureField sigField = (SignatureField)annotation;
                            if (sigField.Name == targetFieldName)
                            {
                                Stream imageStream = sigField.ExtractImage(ImageFormat.Png);
                                if (imageStream != null)
                                {
                                    using (FileStream file = new FileStream(outputImage, FileMode.Create, FileAccess.Write))
                                    {
                                        imageStream.CopyTo(file);
                                    }
                                    Console.WriteLine($"Signature image saved to '{outputImage}'.");
                                    imageExtracted = true;
                                }
                                else
                                {
                                    Console.WriteLine("No image found in the signature field.");
                                }
                                break;
                            }
                        }
                    }
                    if (imageExtracted)
                    {
                        break;
                    }
                }

                if (!imageExtracted)
                {
                    Console.WriteLine($"Signature field '{targetFieldName}' not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}