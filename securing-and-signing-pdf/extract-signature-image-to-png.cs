using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Devices;          // for PngDevice
using Aspose.Pdf.Forms;            // for SignatureField, AppearanceDictionary, XForm
using Aspose.Pdf.Operators;        // for Do operator

class ExtractSignatureImage
{
    static void Main()
    {
        const string inputPdfPath = "signed_document.pdf";
        const string outputPngPath = "signature.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Locate the first signature field in the form collection
            SignatureField sigField = null;
            foreach (var field in pdfDoc.Form.Fields)
            {
                if (field is SignatureField s)
                {
                    sigField = s;
                    break;
                }
            }

            if (sigField == null)
            {
                Console.Error.WriteLine("No signature field found in the document.");
                return;
            }

            // Get the normal appearance XForm from the field's appearance dictionary
            AppearanceDictionary appearances = sigField.Appearance;
            if (!appearances.TryGetValue("N", out XForm normalForm) || normalForm == null)
            {
                Console.Error.WriteLine("Signature field does not contain a normal appearance.");
                return;
            }

            // Create a temporary one‑page PDF that contains only the appearance XForm
            using (Document tempDoc = new Document())
            {
                Page tempPage = tempDoc.Pages.Add();

                // The Forms collection accepts a single XForm argument. Set the name on the XForm first.
                const string formName = "SigForm";
                normalForm.Name = formName;
                tempPage.Resources.Forms.Add(normalForm);

                // Draw the XForm with a Do operator referencing the name we set above.
                tempPage.Contents.Add(new Do(formName));

                // Render the temporary page to PNG using PngDevice
                Resolution resolution = new Resolution(300);
                PngDevice pngDevice = new PngDevice(resolution);
                using (FileStream fs = new FileStream(outputPngPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(tempDoc.Pages[1], fs);
                }
            }
        }

        Console.WriteLine($"Signature image extracted to: {outputPngPath}");
    }
}
