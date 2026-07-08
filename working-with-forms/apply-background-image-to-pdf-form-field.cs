using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "logoField";
        const string imagePath = "brand.png";

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

        using (Document doc = new Document(inputPdf))
        {
            // Attempt to set image for an XFA field
            try
            {
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    doc.Form.XFA.SetFieldImage(fieldName, imgStream);
                }
            }
            catch
            {
                // Fallback to AcroForm field handling
                // doc.Form[index] returns a WidgetAnnotation; cast to Field when possible
                var field = doc.Form[fieldName] as Field;
                if (field != null)
                {
                    // ButtonField and TextBoxField support AddImage which expects System.Drawing.Image
                    if (field is ButtonField btn)
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                        {
                            btn.AddImage(img);
                        }
                    }
                    else if (field is TextBoxField txt)
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                        {
                            txt.AddImage(img);
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine($"Field '{fieldName}' does not support image addition.");
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found in the form.");
                }
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Saved PDF with background image applied to field '{fieldName}'.");
    }
}
