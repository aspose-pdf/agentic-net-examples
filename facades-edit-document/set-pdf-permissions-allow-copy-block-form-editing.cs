using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // added namespace for form field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a minimal PDF that the demo can work with.
        //    The file contains a single page and a simple text box form field.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            Page page = seed.Pages.Add();
            // Add a text box form field (so we can demonstrate that filling is blocked).
            TextBoxField txt = new TextBoxField(page, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "SampleField",
                Value = "Editable text"
            };
            // Register the field with the document's form collection.
            seed.Form.Add(txt, 1);
            seed.Save(inputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Define the required permissions.
        //    - Allow copying of text/images.
        //    - Disallow form filling (editing form fields).
        //    - Disallow annotation modification/creation.
        // ---------------------------------------------------------------------
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
        privilege.AllowCopy = true;                 // enable text/image copying
        privilege.AllowFillIn = false;              // prevent editing form fields
        privilege.AllowModifyAnnotations = false;   // prevent adding/modifying annotations

        // ---------------------------------------------------------------------
        // 3. Apply the privileges using the Facades API.
        // ---------------------------------------------------------------------
        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);   // load the freshly‑created PDF
        fileSecurity.SetPrivilege(privilege);
        fileSecurity.Save(outputPath);
        fileSecurity.Close();
    }
}
