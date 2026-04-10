using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Load PDF bytes – if a physical file does not exist, create a
        //    minimal PDF in memory so the example is self‑contained.
        // ------------------------------------------------------------
        byte[] pdfBytes;
        const string inputPdfPath = "input.pdf";
        if (File.Exists(inputPdfPath))
        {
            pdfBytes = File.ReadAllBytes(inputPdfPath);
        }
        else
        {
            // Create a one‑page PDF on the fly.
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                using (var ms = new MemoryStream())
                {
                    tempDoc.Save(ms);
                    pdfBytes = ms.ToArray();
                }
            }
        }

        // ------------------------------------------------------------
        // 2. Load signature image – if the file is missing, generate a
        //    simple placeholder PNG (100x30, light gray background).
        // ------------------------------------------------------------
        byte[] signatureImageBytes;
        const string signatureImgPath = "signature.png";
        if (File.Exists(signatureImgPath))
        {
            signatureImageBytes = File.ReadAllBytes(signatureImgPath);
        }
        else
        {
            using (var bmp = new Bitmap(200, 50))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.LightGray);
                    g.DrawString("Signature", new System.Drawing.Font("Arial", 12), System.Drawing.Brushes.Black, new System.Drawing.PointF(10, 15));
                }
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    signatureImageBytes = ms.ToArray();
                }
            }
        }

        // ------------------------------------------------------------
        // 3. Load certificate (PFX). This example expects a real
        //    certificate file; if it is missing we abort with a clear
        //    message because signing cannot proceed without a private key.
        // ------------------------------------------------------------
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";
        if (!File.Exists(pfxPath))
        {
            Console.WriteLine($"Certificate file '{pfxPath}' not found. Signing cannot be performed.");
            return;
        }
        byte[] pfxBytes = File.ReadAllBytes(pfxPath);

        // ------------------------------------------------------------
        // 4. Load the PDF from the memory stream, add a signature field
        //    on the first page and sign it using the image as visual
        //    appearance.
        // ------------------------------------------------------------
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (Document doc = new Document(pdfStream))
        {
            // Define rectangle for the signature field (coordinates are in points).
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create and add the signature field to page 1.
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            doc.Pages[1].Annotations.Add(sigField);

            // Prepare the visual appearance (image) for the signature.
            using (MemoryStream imgStream = new MemoryStream(signatureImageBytes))
            {
                PKCS1 pkcs1 = new PKCS1(imgStream)
                {
                    Reason = "Document approved",
                    Location = "New York, USA"
                };

                // Sign the document using the field and the certificate.
                using (MemoryStream pfxStream = new MemoryStream(pfxBytes))
                {
                    sigField.Sign(pkcs1, pfxStream, pfxPassword);
                }
            }

            // Save the signed PDF.
            const string outputPath = "signed_output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF signed and saved as '{outputPath}'.");
        }
    }
}
