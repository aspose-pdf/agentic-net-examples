using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imageOutputDir = "ExtractedImages";
        const string signatureOutputDir = "ExtractedSignatures";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure output directories exist
        Directory.CreateDirectory(imageOutputDir);
        Directory.CreateDirectory(signatureOutputDir);

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // Extract embedded images from each page
            // -----------------------------------------------------------------
            int imageCounter = 1;
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // Save the image to a file (preserve format where possible)
                    string imgPath = Path.Combine(imageOutputDir,
                        $"Page_{page.Number}_Img_{imageCounter}.png");

                    using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs); // XImage.Save writes the image data to the stream
                    }

                    Console.WriteLine($"Saved image: {imgPath}");
                    imageCounter++;
                }
            }

            // -----------------------------------------------------------------
            // Extract visual representation of signature fields (if any)
            // -----------------------------------------------------------------
            if (doc.Form != null && doc.Form.Fields != null)
            {
                // Filter the generic field collection for SignatureField objects
                var signatureFields = doc.Form.Fields.OfType<SignatureField>();
                int sigCounter = 1;
                foreach (SignatureField sigField in signatureFields)
                {
                    // ExtractImage returns a JPEG stream of the signature appearance (if present)
                    using (Stream sigStream = sigField.ExtractImage())
                    {
                        if (sigStream != null)
                        {
                            string sigPath = Path.Combine(signatureOutputDir,
                                $"Signature_{sigCounter}.jpg");

                            using (FileStream fs = new FileStream(sigPath, FileMode.Create, FileAccess.Write))
                            {
                                sigStream.CopyTo(fs);
                            }

                            Console.WriteLine($"Saved signature image: {sigPath}");
                        }
                        else
                        {
                            Console.WriteLine($"Signature field {sigCounter} contains no image.");
                        }
                    }

                    sigCounter++;
                }
            }
        }
    }
}
