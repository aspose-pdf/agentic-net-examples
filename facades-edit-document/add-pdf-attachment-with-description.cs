using System;
using System.IO;
using Aspose.Pdf;

// Input PDF, attachment file and output PDF paths
const string inputPdfPath   = "input.pdf";
const string attachmentPath = "invoice.pdf";          // PDF to attach
const string outputPdfPath  = "output_with_attachment.pdf";

if (!File.Exists(inputPdfPath))
{
    Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
    return;
}
if (!File.Exists(attachmentPath))
{
    Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
    return;
}

// Open the source PDF, add the attachment with description, then save.
using (Document pdfDoc = new Document(inputPdfPath))
{
    // Create a file specification for the attachment using the constructor that accepts
    // the file path and a description. The MIME type is inferred from the file extension
    // (application/pdf for a .pdf file) and cannot be set explicitly in the current API.
    FileSpecification attachmentSpec = new FileSpecification(attachmentPath, "Invoice Document");

    // Add the attachment to the PDF's EmbeddedFiles collection.
    pdfDoc.EmbeddedFiles.Add(attachmentSpec);

    // Save the modified PDF.
    pdfDoc.Save(outputPdfPath);
}

Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");