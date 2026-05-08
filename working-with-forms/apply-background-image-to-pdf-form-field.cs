using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF with a form field
        const string outputPdf  = "output.pdf";         // PDF after applying background image
        const string imagePath  = "brand_logo.png";     // image to use as background

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Try XFA form field background (XFA.SetFieldImage)
            // -----------------------------------------------------------------
            // NOTE: The XFA API is version‑specific. If the current Aspose.Pdf
            // version does not expose Form.Xfa, this block is safely ignored.
            try
            {
                // Attempt to obtain the XFA object via reflection to avoid compile‑time
                // dependency on a property that may not exist in older versions.
                var xfaProp = doc.Form.GetType().GetProperty("Xfa");
                if (xfaProp != null)
                {
                    var xfa = xfaProp.GetValue(doc.Form);
                    // Xfa class has a SetFieldImage(string, Stream) method.
                    var setImgMethod = xfa.GetType().GetMethod("SetFieldImage", new[] { typeof(string), typeof(Stream) });
                    if (setImgMethod != null)
                    {
                        using (FileStream imgStream = File.OpenRead(imagePath))
                        {
                            setImgMethod.Invoke(xfa, new object[] { "logoField", imgStream });
                        }
                    }
                }
            }
            catch (Exception ex) when (ex is NullReferenceException || ex is NotSupportedException)
            {
                // XFA not present or field not XFA – ignore and try AcroForm approach
            }

            // -----------------------------------------------------------------
            // 2. AcroForm field background (TextBoxField.AddImage or ButtonField.AddImage)
            // -----------------------------------------------------------------
            // Retrieve the field by its fully qualified name and cast to the concrete type.
            TextBoxField txtField = doc.Form["logoField"] as TextBoxField;
            if (txtField != null)
            {
                // Load the image as a System.Drawing.Image (required by AddImage).
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                {
                    txtField.AddImage(img);
                }
            }
            else
            {
                // If not a TextBoxField, try a ButtonField.
                ButtonField btnField = doc.Form["logoField"] as ButtonField;
                if (btnField != null)
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                    {
                        btnField.AddImage(img);
                    }
                }
                // Additional field types can be handled here if needed.
            }

            // -----------------------------------------------------------------
            // Save the modified PDF (lifecycle rule: use Document.Save)
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Background image applied and saved to '{outputPdf}'.");
    }
}
