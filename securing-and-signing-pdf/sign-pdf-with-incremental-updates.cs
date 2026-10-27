using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "mycert.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Create a signature field on the first page
                var sigField = new SignatureField(doc, new Rectangle(100, 100, 250, 150));
                doc.Form.Add(sigField);

                // Prepare PKCS#7 signature object – this is where Reason, Location, etc. are set
                var pkcs = new PKCS7(certPath, certPassword)
                {
                    Reason = "Approved",
                    Location = "New York, USA",
                    ContactInfo = "john.doe@example.com"
                };

                // Optional visual appearance – only if the current Aspose.Pdf version provides the class
                // Uncomment the block below when SignatureAppearance is available in the library.
                /*
                pkcs.SignatureAppearance = new SignatureAppearance
                {
                    Text = "Signed by John Doe",
                    BackgroundColor = Color.LightGray,
                    BorderColor = Color.DarkGray,
                    BorderWidth = 1
                };
                */

                // Sign the field with the prepared PKCS#7 object
                sigField.Sign(pkcs);

                // Save the signed PDF. The document remains open for further signatures.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF signed and saved to '{outputPath}'. Additional signatures can be added later.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
