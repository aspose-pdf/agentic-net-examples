using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public class PdfFormProcessor
{
    // Loads a PDF from a byte array, updates its form fields, and returns the modified PDF as a byte array.
    public static byte[] UpdateFormFields(byte[] pdfBytes)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

        // Load the PDF from the supplied byte array.
        using (var inputStream = new MemoryStream(pdfBytes))
        using (var document = new Document(inputStream))
        {
            // Access the form object.
            var form = document.Form;
            if (form != null)
            {
                // Set values for the desired fields. Cast to the appropriate field type if needed.
                if (form["NameField"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }

                if (form["DateField"] is TextBoxField dateField)
                {
                    dateField.Value = DateTime.Now.ToShortDateString();
                }
            }

            // Save the modified PDF into a new memory stream and return its bytes.
            using (var outputStream = new MemoryStream())
            {
                document.Save(outputStream);
                return outputStream.ToArray();
            }
        }
    }

    // Dummy entry point – required when the project is built as an executable.
    public static void Main(string[] args)
    {
        // Intentionally left blank. The UpdateFormFields method can be called from other code.
    }
}