//
// PartyTemplate.cs
//
// Author:
//       Urist_McAurelian <Discord: Urist_McAurelian#2289>
//
// Copyright (c) 2021 Urist_McAurelian
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static XmlTool.Program;

namespace XmlTool {
	//Functionally same result after running through twice... mostly. Trimming can occur for some of the longer PartyTemplates.
	public class PartyTemplateConverter {
		public static void PartyTemplates_CSVtoXML(string fileInput, string fileOutput) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			PartyTemplateRecord record = new PartyTemplateRecord();
			IEnumerable<PartyTemplateRecord> records = csv.EnumerateRecords(record);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("partyTemplates");

				writeHeadderComment(writer);

				foreach (PartyTemplateRecord partyTemplateRecord in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("MBPartyTemplate");

					//Changes

					//Temporary

					//Write
					writer.WriteAttributeString("id", partyTemplateRecord.id);
					if (!partyTemplateRecord.stack0.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack0.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack1.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack1.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack2.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack2.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack3.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack3.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack4.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack4.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack5.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack5.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack6.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack6.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack7.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack7.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack8.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack8.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}
					if (!partyTemplateRecord.stack9.Equals("")) {
						string[] templateStack = partyTemplateRecord.stack9.Split(";");

						if (templateStack.Length > 1) {
							writer.WriteStartElement("stacks");
							for (int i = 0; i < templateStack.Length; i++) {
								switch (i % 3) {
									case 0:
										writer.WriteStartElement("PartyTemplateStack");
										writer.WriteAttributeString("min_value", templateStack[i]);
										break;
									case 1:
										writer.WriteAttributeString("max_value", templateStack[i]);
										break;
									case 2:
										writer.WriteAttributeString("troop", "NPCCharacter." + templateStack[i]);
										writer.WriteEndElement();
										break;
								}
							}
							writer.WriteEndElement();
						}
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public static void PartyTemplates_XMLtoCSV(string xmlInput, string csvOutput) {
			List<PartyTemplateRecord> records = new List<PartyTemplateRecord>();

			using (XmlReader xmlReader = XmlReader.Create(xmlInput)) {
				while (xmlReader.Read()) {
					if (xmlReader.NodeType != XmlNodeType.Element) continue;
					//Console.WriteLine(xmlReader.Name);

					if (xmlReader.Name.Equals("MBPartyTemplate")) {
					NewPartyRecord:
						PartyTemplateRecord record = new PartyTemplateRecord();

						record.id = xmlReader.GetAttribute("id");

						while (xmlReader.Read() && !(xmlReader.NodeType.Equals(XmlNodeType.Element) || xmlReader.NodeType.Equals(XmlNodeType.EndElement))) ;

						if (xmlReader.Name.Equals("MBPartyTemplate")) {
							records.Add(record);

							if (!xmlReader.NodeType.Equals(XmlNodeType.EndElement)) goto NewPartyRecord;
							continue;
						}

						if (!xmlReader.Name.Equals("stacks")) Console.WriteLine("Error: Node name {0} of type {2} belonging to {1} was unexpected!", xmlReader.Name, xmlReader.GetAttribute("id"), xmlReader.NodeType.ToString());

						int counter = 0;
						while (xmlReader.Read() && !(xmlReader.NodeType.Equals(XmlNodeType.EndElement) && xmlReader.Name.Equals("stacks"))) {
							if (!xmlReader.NodeType.Equals(XmlNodeType.Element)) continue;
							if (xmlReader.Name.Equals("stacks")) continue;
							switch (counter) {
								case 0:
									record.stack0 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 1:
									record.stack1 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 2:
									record.stack2 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 3:
									record.stack3 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 4:
									record.stack4 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 5:
									record.stack5 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 6:
									record.stack6 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 7:
									record.stack7 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 8:
									record.stack8 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
								case 9:
									record.stack9 = xmlReader.GetAttribute("min_value") + ";" + xmlReader.GetAttribute("max_value") + ";" + TrimD(xmlReader.GetAttribute("troop"));
									break;
							}
							counter++;
						}
						records.Add(record);
					}
				}
				using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
					csvWriter.WriteRecords(records);
					csvWriter.Flush();
				}
			}
		}
		public class PartyTemplateRecord {
			public string id { get; set; }

			public string stack0 { get; set; }
			public string stack1 { get; set; }
			public string stack2 { get; set; }
			public string stack3 { get; set; }
			public string stack4 { get; set; }
			public string stack5 { get; set; }
			public string stack6 { get; set; }
			public string stack7 { get; set; }
			public string stack8 { get; set; }
			public string stack9 { get; set; }
		}
	}
}
