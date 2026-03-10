# AGENTS - working-with-xml

## Scope
- This folder contains examples for **working-with-xml**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (15/16 files) ← category-specific
- `using Aspose.Pdf.Drawing;` (1/16 files)
- `using Aspose.Pdf.LogicalStructure;` (1/16 files)
- `using Aspose.Pdf.Tagged;` (1/16 files)
- `using Aspose.Pdf.Text;` (1/16 files)
- `using System;` (16/16 files)
- `using System.IO;` (16/16 files)
- `using System.Xml.Linq;` (3/16 files)
- `using System.Xml;` (2/16 files)
- `using System.Xml.Xsl;` (1/16 files)

## Common Code Pattern

Most files in this category load documents with `XmlLoadOptions`:

```csharp
XmlLoadOptions options = new XmlLoadOptions();
using (Document doc = new Document("input.pdf", options))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [Apply-XSLT-transformation-to-convert-an-XML-file-into-a-PDF-...](./Apply-XSLT-transformation-to-convert-an-XML-file-into-a-PDF-document-taking-XML-as-input-and-produc.cs) | `XmlLoadOptions` | Apply XSLT transformation to convert an XML file into a PDF document taking X... |
| [Bind-XML-data-to-a-PDF-template-and-generate-a-PDF-document-...](./Bind-XML-data-to-a-PDF-template-and-generate-a-PDF-document-accepting-XML-input-and-producing-PDF-o.cs) |  | Bind XML data to a PDF template and generate a PDF document accepting XML inp... |
| [Create-a-PDF-document-from-an-XML-source-converting-XML-inpu...](./Create-a-PDF-document-from-an-XML-source-converting-XML-input-into-a-PDF-output-file.cs) | `XmlLoadOptions` | Create a PDF document from an XML source converting XML input into a PDF outp... |
| [Create-a-PDF-file-from-XML-data-using-the-defined-PDF-XML-sc...](./Create-a-PDF-file-from-XML-data-using-the-defined-PDF-XML-schema-producing-PDF-output.cs) | `XmlLoadOptions` | Create a PDF file from XML data using the defined PDF XML schema producing PD... |
| [Create-a-PDF-file-from-XSL-FO-markup-by-processing-XSL-FO-in...](./Create-a-PDF-file-from-XSL-FO-markup-by-processing-XSL-FO-input-and-producing-PDF-output.cs) | `XslFoLoadOptions` | Create a PDF file from XSL FO markup by processing XSL FO input and producing... |
| [Create-a-page-definition-by-providing-an-XML-configuration-t...](./Create-a-page-definition-by-providing-an-XML-configuration-that-specifies-page-dimensions-margins.cs) | `TextFragment` | Create a page definition by providing an XML configuration that specifies pag... |
| [Generate-a-PDF-document-from-an-XML-source-applying-an-XSLT-...](./Generate-a-PDF-document-from-an-XML-source-applying-an-XSLT-stylesheet-using-XML-as-input-and-produ.cs) | `XmlLoadOptions` | Generate a PDF document from an XML source applying an XSLT stylesheet using ... |
| [Insert-a-Table-element-into-an-existing-XML-document-adherin...](./Insert-a-Table-element-into-an-existing-XML-document-adhering-to-the-XML-schema.cs) |  | Insert a Table element into an existing XML document adhering to the XML schema |
| [Insert-an-HtmlFragment-element-into-the-target-XML-file-adhe...](./Insert-an-HtmlFragment-element-into-the-target-XML-file-adhering-to-correct-XML-formatting-conventi.cs) |  | Insert an HtmlFragment element into the target XML file adhering to correct X... |
| [Insert-graphic-elements-onto-a-page-by-supplying-an-XML-defi...](./Insert-graphic-elements-onto-a-page-by-supplying-an-XML-definition-that-specifies-their-properties.cs) | `Rectangle`, `Ellipse` | Insert graphic elements onto a page by supplying an XML definition that speci... |
| [Modify-XML-nodes-at-runtime-by-programmatically-updating-con...](./Modify-XML-nodes-at-runtime-by-programmatically-updating-content-while-maintaining-a-valid-XML-struc.cs) |  | Modify XML nodes at runtime by programmatically updating content while mainta... |
| [Read-an-XML-file-from-the-given-location-and-parse-its-conte...](./Read-an-XML-file-from-the-given-location-and-parse-its-contents-according-to-the-XML-schema.cs) |  | Read an XML file from the given location and parse its contents according to ... |
| [Specify-the-image-directory-when-converting-XML-to-PDF-using...](./Specify-the-image-directory-when-converting-XML-to-PDF-using-XML-as-input-and-PDF-as-output.cs) | `XslFoLoadOptions` | Specify the image directory when converting XML to PDF using XML as input and... |
| [Transform-XML-data-using-XSLT-to-generate-a-PDF-document-as-...](./Transform-XML-data-using-XSLT-to-generate-a-PDF-document-as-the-final-output.cs) | `XmlLoadOptions` | Transform XML data using XSLT to generate a PDF document as the final output |
| [Transform-XSL-FO-markup-and-parameters-into-a-PDF-file-produ...](./Transform-XSL-FO-markup-and-parameters-into-a-PDF-file-producing-PDF-output-from-XSL-FO-input.cs) | `XslFoLoadOptions` | Transform XSL FO markup and parameters into a PDF file producing PDF output f... |
| [Transform-the-XML-file-to-PDF-by-applying-an-XSL-stylesheet-...](./Transform-the-XML-file-to-PDF-by-applying-an-XSL-stylesheet-that-generates-an-HTML-layout-before-ren.cs) | `XmlLoadOptions` | Transform the XML file to PDF by applying an XSL stylesheet that generates an... |

## Category Statistics
- Total examples: 16

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.BindXml(string, string)`
- `Aspose.Pdf.Document.Save(string)`

### Rules
- Create a new {doc} (Aspose.Pdf.Document) instance, then call {doc}.BindXml({string_literal} xmlPath, {string_literal} xsltPath) to populate the document from XML/XSLT.
- After binding, invoke {doc}.Save({string_literal} outputPath) to write the generated PDF to disk.
- Create a new {doc} = new Aspose.Pdf.Document();
- Bind an XML file and an XSLT stylesheet to the document using {doc}.BindXml({string_literal} xmlPath, {string_literal} xsltPath);
- Persist the transformed PDF with {doc}.Save({string_literal} outputPath);

### Warnings
- BindXml expects valid file paths to existing XML and XSLT files; missing files will raise an exception.
- The example creates an empty document before binding; binding after adding pages may produce unexpected layout.
- BindXml requires that the provided XML and XSLT files are well‑formed and compatible; otherwise an exception will be thrown.

## General Tips
- See parent [agents.md](../agents.md) for repository-level patterns, conventions, and anti-patterns
- Review code examples in this folder for working-with-xml patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-10 | Run: `20260310_143158_d8bafe`
<!-- AUTOGENERATED:END -->
