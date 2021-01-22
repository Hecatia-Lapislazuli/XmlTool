using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using CsvHelper;

namespace CSV_to_XML {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("~~~NEW PROGRAM~~~");

			//Console.WriteLine("What is your root directory?");
			string root = AppDomain.CurrentDomain.BaseDirectory;
			string DSC = Path.DirectorySeparatorChar.ToString(); //For berevity's sake. 
			//Console.WriteLine("Please input a address for the Settlement csv file: ");

			Directory.CreateDirectory(root+"Output");
			Directory.CreateDirectory(root + "Output" + DSC + "Languages");

			XmlWriterSettings localizationSettings = new XmlWriterSettings();
			localizationSettings.Indent = true;

			using (XmlWriter module_stringsWriter = XmlWriter.Create(root + "Output" + DSC + "module_strings.xml", localizationSettings)) {
				module_stringsWriter.WriteStartElement("base");
				module_stringsWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
				module_stringsWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
				module_stringsWriter.WriteAttributeString("type", "string");
				module_stringsWriter.WriteStartElement("strings");

				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_settlements_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Settlements.csv")) settlement_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Settlements.csv", root + "Output" + DSC + "Touhou XML Data - Settlements.csv", root + "Output" + DSC + "settlements.xml", root + "Data" + DSC + "scene.xscene", root + "Output" + DSC + "scene.xscene", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}

				//string root = "O:" + DSC + "Games" + DSC + "SteamLibrary" + DSC + "steamapps" + DSC + "common" + DSC + "Mount & Blade II Bannerlord" + DSC + "Modules" + DSC + "TouhouAnalepsia" + DSC + "";
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_heroes_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Heroes.csv")) heroes_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Heroes.csv", root + "Output" + DSC + "heroes.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_lords_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - NPCCharacters.csv")) NPCCharacters_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - NPCCharacters.csv", root + "Output" + DSC + "lords.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_companions_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - NPCCharacters - Companions.csv")) NPCCharacters_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - NPCCharacters - Companions.csv", root + "Output" + DSC + "companions.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_spspecialcharacters_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - NPCCharacters - Units.csv")) NPCCharacters_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - NPCCharacters - Units.csv", root + "Output" + DSC + "spspecialcharacters.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_spkingdoms_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Kingdoms.csv")) Kingdoms_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Kingdoms.csv", root + "Output" + DSC + "spkingdoms.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_spclans_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Clans.csv")) Clans_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Clans.csv", root + "Output" + DSC + "spclans.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				using (XmlWriter localizationWriter = XmlWriter.Create(root + "Output" + DSC + "Languages" + DSC + "std_spcultures_xml.xml", localizationSettings)) {
					initializeLocalizationWriter(localizationWriter);
					if (File.Exists(root + "Data" + DSC + "Touhou XML Data - Cultures.csv")) Cultures_CSVtoXML(root + "Data" + DSC + "Touhou XML Data - Cultures.csv", root + "Output" + DSC + "spcultures.xml", localizationWriter, module_stringsWriter);
					localizationWriter.WriteEndElement();
					localizationWriter.WriteEndElement();
				}
				module_stringsWriter.WriteEndElement();
				module_stringsWriter.WriteEndElement();
			}
		}
		public static void settlement_CSVtoXML(string csvInput, string csvOutput, string xmlOutput, string sceneFileInput, string sceneFileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader csvReader = new StreamReader(csvInput);
			CsvReader csv = new CsvReader(csvReader, CultureInfo.InvariantCulture);

			IEnumerable<SettlementRecord> enumerableRecords = csv.GetRecords<SettlementRecord>();

			List<SettlementRecord> records = enumerableRecords.ToList();

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
            settings.IndentChars = "\t";

			XmlReaderSettings settings1 = new XmlReaderSettings();

			using (XmlReader sceneReader = XmlReader.Create(sceneFileInput, settings1)) {
				using (XmlWriter sceneWriter = XmlWriter.Create(sceneFileOutput, settings)) {
                    while (sceneReader.Read()) {
                        if (sceneReader.NodeType == XmlNodeType.Element) {
							if (sceneReader.Name.Equals("scene")) {
								sceneWriter.WriteStartElement("scene");
								sceneWriter.WriteAttributes(sceneReader, false);
								//Do stuff here

								while (sceneReader.Read()) {
									if (sceneReader.NodeType.Equals(XmlNodeType.Element)) {
										Console.WriteLine("Currently at node {0}", sceneReader.Name);
										if (sceneReader.Name.Equals("entities")) {
											sceneWriter.WriteStartElement("entities");
											sceneWriter.WriteAttributes(sceneReader, false);
											List<SettlementRecord> addedRecords = new List<SettlementRecord>();



											while (sceneReader.Read()) {
												if (sceneReader.NodeType.Equals(XmlNodeType.EndElement) && sceneReader.Name.Equals("entities")) {
													//Handle non-initialized entities

													int positionCounterX = 1;
													int positionCounterY = 1;

													foreach (SettlementRecord record in records.Except(addedRecords)) {
														//break;//Get rid of this for the more so... experimental portions of the code.

														sceneWriter.WriteStartElement("game_entity");

														sceneWriter.WriteAttributeString("name", record.id);

														switch (record.GetSettlementType()) {
															case "castle":
																sceneWriter.WriteAttributeString("old_prefab_name", "map_icon_castle_battania");
																break;
															case "village":
																sceneWriter.WriteAttributeString("old_prefab_name", "map_icon_full_battania_village");
																break;
															case "town":
																sceneWriter.WriteAttributeString("old_prefab_name", "__empty_object");
																break;
														}
														sceneWriter.WriteAttributeString("season_mask", "255");

														//Nodes - Start

														//Transform - Start
														sceneWriter.WriteStartElement("transform");

														string[] positions = new string[3];
														positions[2] = "50.000"; //Constant Parameter

														if (record.posX.Equals("") || record.posY.Equals("")) {
															if(record.GetSettlementType().Equals("village")) {//record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2)
																//if (record.id.Contains("castle_village_")) {
																//	writer.WriteAttributeString(null, "trade_bound", null, record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
																//	writer.WriteAttributeString(null, "bound", null, record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
																//} else {
																//	writer.WriteAttributeString(null, "trade_bound", null, record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
																//	writer.WriteAttributeString(null, "bound", null, record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
																//}

																foreach(SettlementRecord r in records) {
																	if (r.id.Equals(record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2)) || r.id.Equals(record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2))) {
																		if (r.auto_assigned || r.posX.Equals("") || r.posY.Equals("")) break;

																		double centerX = Double.Parse(r.posX);
																		double centerY = Double.Parse(r.posY);

																		int offsetX = (r.auto_position_counter%3)-1;
																		int offsetY = (int) Math.Floor(r.auto_position_counter/3d)-1;

																		positions[0] = (4 * offsetX + centerX).ToString();
																		positions[1] = (4 * offsetY + centerY).ToString();

																		record.posX = positions[0];
																		record.posY = positions[1];

																		r.auto_position_counter++;
																		if (r.auto_position_counter == 4) r.auto_position_counter++;
																		
																		goto AfterPositionHandling;
																	}
                                                                }
															}

															record.auto_assigned = true;

															positions[0] = (4*positionCounterX).ToString();
															positions[1] = (4*positionCounterY).ToString();

															record.posX = positions[0];
															record.posY = positions[1];

															positionCounterX++;
															if (positionCounterX > 200) {
																positionCounterX = 1;
																positionCounterY++;
															}
														} else {
															positions[0] = record.posX;
															positions[1] = record.posY;
														}

                                                    AfterPositionHandling:
														sceneWriter.WriteAttributeString("position", positions[0] + ", " + positions[1] + ", " + positions[2]);
														sceneWriter.WriteAttributeString("rotation_euler", "0.000, 0.000, -1.601");
														//Skip Scale

														sceneWriter.WriteEndElement();
														//Transform - End

														//Physics - Start
														sceneWriter.WriteStartElement("physics");
														sceneWriter.WriteAttributeString("mass", "1.000");
														sceneWriter.WriteEndElement();
														//Physics - End

														//Children - Start
														sceneWriter.WriteStartElement("children");
														switch(record.GetSettlementType()) {
															case "castle":
																using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + "sampleCastleChildren.xml", settings1)) {
																	while (sample.Read() && (!sample.NodeType.Equals(XmlNodeType.Element) || (sample.NodeType.Equals(XmlNodeType.Element) && !sample.Name.Equals("child_entities"))));
																	while (sample.Read()) {
																		if(sample.NodeType.Equals(XmlNodeType.Element)) {
																			if (sample.Name.Equals("child_entities")) continue;
																			sceneWriter.WriteNode(sample, false);
                                                                        }
                                                                    }
                                                                }
																break;
															case "village":
																using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + "sampleVillageChildren.xml", settings1)) {
																	while (sample.Read() && (!sample.NodeType.Equals(XmlNodeType.Element) || (sample.NodeType.Equals(XmlNodeType.Element) && !sample.Name.Equals("child_entities")))) ;
																	while (sample.Read()) {
																		if (sample.NodeType.Equals(XmlNodeType.Element)) {
																			if (sample.Name.Equals("child_entities")) continue;
																			sceneWriter.WriteNode(sample, false);
																		}
																	}
																}
																break;
															case "town":
																using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory + "sampleTownChildren.xml", settings1)) {
																	while (sample.Read() && (!sample.NodeType.Equals(XmlNodeType.Element) || (sample.NodeType.Equals(XmlNodeType.Element) && !sample.Name.Equals("child_entities")))) ;
																	while (sample.Read()) {
																		if (sample.NodeType.Equals(XmlNodeType.Element)) {
																			if (sample.Name.Equals("child_entities")) continue;
																			sceneWriter.WriteNode(sample, false);
																		}
																	}
																}
																break;
															default:
																Console.WriteLine("You... Shouldn't be here. {0}", record.GetSettlementType());
																break;
														}
														sceneWriter.WriteEndElement();
														//Children - End

														sceneWriter.WriteEndElement();


														//OTHER METHOD STARTS HERE
														/*
														using (XmlReader sample = XmlReader.Create(AppDomain.CurrentDomain.BaseDirectory+"sampleCastle.xml", settings1)) {
															while (sample.Read()) {
																if (!sample.NodeType.Equals(XmlNodeType.Element)) continue;
																if (!sample.Name.Equals("game_entity")) continue;
																//if (sample.GetAttribute("name") == null) continue;

																//Console.WriteLine("Hello? {0}", record.id);

																sceneWriter.WriteStartElement("game_entity");

																sceneWriter.WriteAttributeString("name", record.id);

																if (sample.GetAttribute("old_prefab_name") != null) sceneWriter.WriteAttributeString("old_prefab_name", sample.GetAttribute("old_prefab_name"));
																if (sample.GetAttribute("prefab") != null) sceneWriter.WriteAttributeString("prefab", sample.GetAttribute("prefab"));
																if (sample.GetAttribute("season_mask") != null) sceneWriter.WriteAttributeString("season_mask", sample.GetAttribute("season_mask"));

																while (sample.Read()) {
																	if (sample.NodeType.Equals(XmlNodeType.EndElement) && sample.Name.Equals("game_entity")) {
																		sceneWriter.WriteEndElement();
																		break;
																	}
																	//if (sample.Name.Equals("children")) Console.WriteLine("Marco 4"); ;
																	//Console.WriteLine("We're reading a {1} node of type {0}.", sample.Name, sample.NodeType);
																	if (sample.NodeType.Equals(XmlNodeType.Element)) {
																		//Console.WriteLine("Marco 3");
																		switch (sample.Name) {
																			case "transform":
																				sceneWriter.WriteStartElement("transform");

																				string[] positions = sample.GetAttribute("position").Split(", ");

																				if(record.posX.Equals("") || record.posY.Equals("")) {
																					positions[0] = positionCounterX.ToString();
																					positions[1] = positionCounterY.ToString();

																					record.posX = positions[0];
																					record.posY = positions[1];

																					positionCounterX++;
																					if(positionCounterX > 200) {
																						positionCounterX = 1;
																						positionCounterY++;
                                                                                    }
																				} else {
																					positions[0] = record.posX;
																					positions[1] = record.posY;
																				}


																				sceneWriter.WriteAttributeString("position", positions[0] + ", " + positions[1] + ", " + positions[2]);

																				sceneWriter.WriteAttributeString("rotation_euler", "0.000, 0.000, -1.601");

																				//if (sample.GetAttribute("rotation_euler") != null) sceneWriter.WriteAttributeString("rotation_euler", sample.GetAttribute("rotation_euler"));
																				//if (sample.GetAttribute("scale") != null) sceneWriter.WriteAttributeString("scale", sample.GetAttribute("scale"));

																				sceneWriter.WriteEndElement();
																				break;
																			case "physics":
																				sceneWriter.WriteNode(sample, false);
																				break;
																			case "children":
																				sceneWriter.WriteStartElement("children");
																				//sceneWriter.WriteAttributes(sample, false);
																				while(sample.Read()) {
																					if(sample.NodeType.Equals(XmlNodeType.EndElement)&&sample.Name.Equals("children")) {
																						//Console.WriteLine("Marco 1");
																						sceneWriter.WriteEndElement();
																						break;
																					}
																					//Console.WriteLine("Marco 2");
																					if (sample.NodeType.Equals(XmlNodeType.Element) && sample.Name.Equals("game_entity")) sceneWriter.WriteNode(sample, false);
																				}
																				break;
																			default:
																				Console.WriteLine("You shouldn't be seeing this. We're reading a type {0}.", sample.Name);
																				//sceneWriter.WriteNode(sample, false);
																				return;
																		}
																	}
																}
															}
														}
														*/
													}
													//Stop
													sceneWriter.WriteEndElement();
													break;
												}

												if (sceneReader.NodeType.Equals(XmlNodeType.Element)) {
													//Do stuff here 2: Electric Boogaloo
													if (!sceneReader.Name.Equals("game_entity")) continue;
													if (sceneReader.GetAttribute("name") == null) continue;


													if (sceneReader.GetAttribute("name").StartsWith("castle_") || sceneReader.GetAttribute("name").StartsWith("village_") || sceneReader.GetAttribute("name").StartsWith("town_")) {
														/* // Check to ensure all attributes are taken care of.
														int count=1;
														if (sceneReader.GetAttribute("old_prefab_name") != null) count++;
														if (sceneReader.GetAttribute("prefab") != null) count++;
														if (sceneReader.GetAttribute("season_mask") != null) count++;
														if (sceneReader.AttributeCount != count) {
															for (int i = 0; i < sceneReader.AttributeCount; i++) Console.Write((i + 1) + ": " + sceneReader.GetAttribute(i) + "\n");
														}
														//*/
														SettlementRecord record=null;
														foreach(SettlementRecord r in records) {
															if (r.id_scene.Equals(sceneReader.GetAttribute("name"))) {
																record = r;
																break;
															}
														}
														foreach (SettlementRecord r in records) {
															if (r.id.Equals(sceneReader.GetAttribute("name"))) {
																if(record!=null && record != r) {
																	throw new Exception("There already exists an id "+r.id+".");
                                                                }
																record = r;
																break;
															}
														}

														if (record!=null) {
															addedRecords.Add(record);


															sceneWriter.WriteStartElement("game_entity");

															sceneWriter.WriteAttributeString("name", record.id);

															if (sceneReader.GetAttribute("old_prefab_name") != null) sceneWriter.WriteAttributeString("old_prefab_name", sceneReader.GetAttribute("old_prefab_name"));
															if (sceneReader.GetAttribute("prefab") != null) sceneWriter.WriteAttributeString("prefab", sceneReader.GetAttribute("prefab"));
															if (sceneReader.GetAttribute("season_mask") != null) sceneWriter.WriteAttributeString("season_mask", sceneReader.GetAttribute("season_mask"));

															while (sceneReader.Read()) {
																if (sceneReader.NodeType.Equals(XmlNodeType.EndElement) && sceneReader.Name.Equals("game_entity")) {
																	sceneWriter.WriteEndElement();
																	break;
																}
																if (sceneReader.NodeType.Equals(XmlNodeType.Element)) {
																	switch (sceneReader.Name) {
																		case "transform":
																			/*
																			int count = 0;
																			if (sceneReader.GetAttribute("position") != null) count++;
																			if (sceneReader.GetAttribute("rotation_euler") != null) count++;
																			if (count!=2) {
																				for (int i = 0; i < sceneReader.AttributeCount; i++) Console.Write((i + 1) + " - "+record.id+": " + sceneReader.GetAttribute(i) + "\n");
																			}
																			*/
																			sceneWriter.WriteStartElement("transform");

																			string[] positions = sceneReader.GetAttribute("position").Split(", ");

																			record.posX = positions[0];
																			record.posY = positions[1];

																			sceneWriter.WriteAttributeString("position", sceneReader.GetAttribute("position"));
																			if (sceneReader.GetAttribute("rotation_euler") != null) sceneWriter.WriteAttributeString("rotation_euler", sceneReader.GetAttribute("rotation_euler"));
																			if (sceneReader.GetAttribute("scale") != null) sceneWriter.WriteAttributeString("scale", sceneReader.GetAttribute("scale"));

																			sceneWriter.WriteEndElement();
																			break;
																		default:
																			sceneWriter.WriteNode(sceneReader, false);
																			break;
																	}
																}
															}
														} else { //TODO Handle excess castles and whatnot.
															sceneReader.Skip();
														}
													} else {
														sceneWriter.WriteNode(sceneReader, false);
													}



													//Stop doing stuff 2: Electric Boogaloo
												}
											}
										} else {
											sceneWriter.WriteNode(sceneReader, false);
										}
									}
								}

								//Stop doing stuff
								sceneWriter.WriteEndElement();
							}
						}
                    }

                    sceneWriter.Flush();
				}
			}

			using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
				csvWriter.WriteRecords(records);
				csvWriter.Flush();
			}

			using (XmlWriter writer = XmlWriter.Create(xmlOutput, settings)) {
				writer.WriteStartElement("Settlements");
				foreach (SettlementRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Settlement");

					//Changes
					record.Comp_Town_is_castle = record.Comp_Town_is_castle.ToLower();

					//Defaults
					if (record.culture.Equals("")) record.culture = "youkai";

					//Temporary


					//Write
					writer.WriteAttributeString(null, "id", null, record.id);
					writer.WriteAttributeString(null, "name", null, "{=Settlements.Settlement." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Settlements.Settlement." + record.id + ".name", record.name);

					if (!record.owner.Equals("")) writer.WriteAttributeString(null, "owner", null, "Faction." + record.owner);
					writer.WriteAttributeString(null, "posX", null, record.posX);
					writer.WriteAttributeString(null, "posY", null, record.posY);
					if (!record.prosperity.Equals("")) writer.WriteAttributeString(null, "prosperity", null, record.prosperity);
					if (!record.culture.Equals("")) writer.WriteAttributeString(null, "culture", null, "Culture."+record.culture);
					if (!record.gate_posX.Equals("")) writer.WriteAttributeString(null, "gate_posX", null, record.gate_posX);
					if (!record.gate_posX.Equals("")) writer.WriteAttributeString(null, "gate_posY", null, record.gate_posY);
					if (!record.gate_rotation.Equals("")) writer.WriteAttributeString(null, "gate_rotation", null, record.gate_rotation);
					if (!record.type.Equals("")) writer.WriteAttributeString(null, "type", null, record.type);
					if (!record.text.Equals("")) writer.WriteAttributeString(null, "text", null, "{=Settlements.Settlement." + record.id + ".text}" + record.text);

					if (!record.text.Equals("")) writeLocalizationNode(localizationWriter, "Settlements.Settlement." + record.id + ".text", record.text);

					if (!record.Comp_Town_is_castle.Equals("") || !record.Comp_Village_village_type.Equals("") || !record.Comp_Hideout_map_icon.Equals("")) {
						writer.WriteStartElement("Components");

						if (!record.Comp_Town_is_castle.Equals("")) { // Town or Castle
							writer.WriteStartElement("Town");

							//Set up defaults
							if (record.Comp_Town_level.Equals("")) record.Comp_Town_level = "1";
							if (record.Comp_Town_background_crop_position.Equals("")) record.Comp_Town_background_crop_position = "0.0";
							if (record.Comp_Town_background_mesh.Equals("")) record.Comp_Town_background_mesh = "menu_empire_1";
							if (record.Comp_Town_wait_mesh.Equals("")) record.Comp_Town_wait_mesh = "wait_empire_town";
							if (record.Comp_Town_gate_rotation.Equals("")) record.Comp_Town_gate_rotation = "0.908";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id.Replace("_", "_comp_"));
							writer.WriteAttributeString(null, "is_castle", null, record.Comp_Town_is_castle);
							writer.WriteAttributeString(null, "level", null, record.Comp_Town_level);
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Town_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Town_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Town_wait_mesh);
							writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Town_gate_rotation);

							writer.WriteEndElement();
						}

						if (!record.Comp_Village_village_type.Equals("")) { // Village
							writer.WriteStartElement("Village");

							//Set up defaults
							if (record.Comp_Village_hearth.Equals("")) record.Comp_Village_hearth = "200"; // Seeded Random?
							if (record.Comp_Village_background_crop_position.Equals("")) record.Comp_Village_background_crop_position = "0.0";
							if (record.Comp_Village_background_mesh.Equals("")) record.Comp_Village_background_mesh = "gui_bg_village_empire";
							if (record.Comp_Village_castle_background_mesh.Equals("")) record.Comp_Village_castle_background_mesh = "gui_bg_castle_empire";
							if (record.Comp_Village_wait_mesh.Equals("")) record.Comp_Village_wait_mesh = "wait_empire_village";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id.Replace("village_", "village_comp_"));
							writer.WriteAttributeString(null, "village_type", null, "VillageType." + record.Comp_Village_village_type);
							if (!record.Comp_Village_gate_rotation.Equals("")) writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Village_gate_rotation);
							writer.WriteAttributeString(null, "hearth", null, record.Comp_Village_hearth);
							if (record.id.Contains("castle_village_")) {
								writer.WriteAttributeString(null, "trade_bound", null, "Settlement." + record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
								writer.WriteAttributeString(null, "bound", null, "Settlement." + record.id.Replace("castle_village_", "castle_").Remove(record.id.Replace("castle_village_", "castle_").Length - 2, 2));
							} else {
								writer.WriteAttributeString(null, "trade_bound", null, "Settlement." + record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
								writer.WriteAttributeString(null, "bound", null, "Settlement." + record.id.Replace("village_", "town_").Remove(record.id.Replace("village_", "town_").Length - 2, 2));
							}
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Village_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Village_background_mesh);
							writer.WriteAttributeString(null, "castle_background_mesh", null, record.Comp_Village_castle_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Village_wait_mesh);

							writer.WriteEndElement();
						}

						if (!record.Comp_Hideout_map_icon.Equals("")) { // Hideout
							writer.WriteStartElement("Hideout");

							//Set up defaults
							if (record.Comp_Hideout_scene_name.Equals("")) record.Comp_Hideout_scene_name = "bandit_forest";
							if (record.Comp_Hideout_background_crop_position.Equals("")) record.Comp_Hideout_background_crop_position = "0.0";
							if (record.Comp_Hideout_background_mesh.Equals("")) record.Comp_Hideout_background_mesh = "empire_twn_scene_bg";
							if (record.Comp_Hideout_wait_mesh.Equals("")) record.Comp_Hideout_wait_mesh = "wait_hideout_forest";
							if (record.Comp_Hideout_gate_rotation.Equals("")) record.Comp_Hideout_gate_rotation = "0.0";

							//Write
							writer.WriteAttributeString(null, "id", null, record.id);
							writer.WriteAttributeString(null, "map_icon", null, record.Comp_Hideout_map_icon);
							writer.WriteAttributeString(null, "scene_name", null, record.Comp_Hideout_scene_name);
							writer.WriteAttributeString(null, "background_crop_position", null, record.Comp_Town_background_crop_position);
							writer.WriteAttributeString(null, "background_mesh", null, record.Comp_Hideout_background_mesh);
							writer.WriteAttributeString(null, "wait_mesh", null, record.Comp_Town_wait_mesh);
							writer.WriteAttributeString(null, "gate_rotation", null, record.Comp_Hideout_gate_rotation);

							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.Locations_complex_template.Equals("")) {
						writer.WriteStartElement("Locations");
						writer.WriteAttributeString(null, "complex_template", null, "LocationComplexTemplate." + record.Locations_complex_template);
						if (!record.Locations_Location0_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location0_id);
							if (!record.Locations_Location0_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location0_scene_name);
							if (!record.Locations_Location0_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location0_scene_name_1);
							if (!record.Locations_Location0_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location0_scene_name_2);
							if (!record.Locations_Location0_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location0_scene_name_3);
							if (!record.Locations_Location0_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location0_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location1_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location1_id);
							if (!record.Locations_Location1_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location1_scene_name);
							if (!record.Locations_Location1_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location1_scene_name_1);
							if (!record.Locations_Location1_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location1_scene_name_2);
							if (!record.Locations_Location1_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location1_scene_name_3);
							if (!record.Locations_Location1_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location1_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location2_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location2_id);
							if (!record.Locations_Location2_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location2_scene_name);
							if (!record.Locations_Location2_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location2_scene_name_1);
							if (!record.Locations_Location2_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location2_scene_name_2);
							if (!record.Locations_Location2_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location2_scene_name_3);
							if (!record.Locations_Location2_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location2_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location3_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location3_id);
							if (!record.Locations_Location3_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location3_scene_name);
							if (!record.Locations_Location3_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location3_scene_name_1);
							if (!record.Locations_Location3_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location3_scene_name_2);
							if (!record.Locations_Location3_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location3_scene_name_3);
							if (!record.Locations_Location3_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location3_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location4_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location4_id);
							if (!record.Locations_Location4_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location4_scene_name);
							if (!record.Locations_Location4_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location4_scene_name_1);
							if (!record.Locations_Location4_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location4_scene_name_2);
							if (!record.Locations_Location4_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location4_scene_name_3);
							if (!record.Locations_Location4_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location4_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location5_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location5_id);
							if (!record.Locations_Location5_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location5_scene_name);
							if (!record.Locations_Location5_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location5_scene_name_1);
							if (!record.Locations_Location5_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location5_scene_name_2);
							if (!record.Locations_Location5_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location5_scene_name_3);
							if (!record.Locations_Location5_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location5_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location6_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location6_id);
							if (!record.Locations_Location6_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location6_scene_name);
							if (!record.Locations_Location6_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location6_scene_name_1);
							if (!record.Locations_Location6_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location6_scene_name_2);
							if (!record.Locations_Location6_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location6_scene_name_3);
							if (!record.Locations_Location6_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location6_max_prosperity);

							writer.WriteEndElement();
						}
						if (!record.Locations_Location7_id.Equals("")) {
							writer.WriteStartElement("Location");

							writer.WriteAttributeString(null, "id", null, record.Locations_Location7_id);
							if (!record.Locations_Location7_scene_name.Equals("")) writer.WriteAttributeString(null, "scene_name", null, record.Locations_Location7_scene_name);
							if (!record.Locations_Location7_scene_name_1.Equals("")) writer.WriteAttributeString(null, "scene_name_1", null, record.Locations_Location7_scene_name_1);
							if (!record.Locations_Location7_scene_name_2.Equals("")) writer.WriteAttributeString(null, "scene_name_2", null, record.Locations_Location7_scene_name_2);
							if (!record.Locations_Location7_scene_name_3.Equals("")) writer.WriteAttributeString(null, "scene_name_3", null, record.Locations_Location7_scene_name_3);
							if (!record.Locations_Location7_max_prosperity.Equals("")) writer.WriteAttributeString(null, "max_prosperity", null, record.Locations_Location7_max_prosperity);

							writer.WriteEndElement();
						}
						writer.WriteEndElement();

					}

					bool automatic_common_areas_village = true;

					if (!record.CommonAreas_Area0_type.Equals("") || !record.CommonAreas_Area1_type.Equals("") || !record.CommonAreas_Area2_type.Equals("") || (!record.Comp_Village_village_type.Equals("") && automatic_common_areas_village)) {
						writer.WriteStartElement("CommonAreas");

						if (!record.Comp_Village_village_type.Equals("") && automatic_common_areas_village) {
							if (record.CommonAreas_Area0_type.Equals("")) {
								record.CommonAreas_Area0_type = "Pasture";
								record.CommonAreas_Area0_name = "Pasture";
							}
							if (record.CommonAreas_Area1_type.Equals("")) {
								record.CommonAreas_Area1_type = "Thicket";
								record.CommonAreas_Area1_name = "Thicket";
							}
							if (record.CommonAreas_Area2_type.Equals("")) {
								record.CommonAreas_Area2_type = "Bog";
								record.CommonAreas_Area2_name = "Bog";
							}
						}

						if (!record.CommonAreas_Area0_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area0_type);
							writer.WriteAttributeString(null, "name", null, "{=CommonAreas.Area." + record.CommonAreas_Area0_name + "}" + record.CommonAreas_Area0_name);
							writer.WriteEndElement();
						}
						if (!record.CommonAreas_Area1_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area1_type);
							writer.WriteAttributeString(null, "name", null, "{=CommonAreas.Area." + record.CommonAreas_Area0_name + "}" + record.CommonAreas_Area1_name);
							writer.WriteEndElement();
						}
						if (!record.CommonAreas_Area2_type.Equals("")) {
							writer.WriteStartElement("Area");
							writer.WriteAttributeString(null, "type", null, record.CommonAreas_Area2_type);
							writer.WriteAttributeString(null, "name", null, "{=CommonAreas.Area." + record.CommonAreas_Area0_name + "}" + record.CommonAreas_Area2_name);
							writer.WriteEndElement();
						}
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.Flush();
			}
		}
        public class SettlementRecord {
			//Basic
			public string id { get; set; }
			public string id_scene { get; set; }
			public string name { get; set; }
			public string owner { get; set; }
			public string posX { get; set; }
			public string posY { get; set; }
			public string prosperity { get; set; }
			public string culture { get; set; }
			public string gate_posX { get; set; }
			public string gate_posY { get; set; }
			public string gate_rotation { get; set; }
			public string type { get; set; }
			public string text { get; set; }

			//Components
			//Town
			//public string Comp_Town_id { get; set; } //Handle this dynamically
			public string Comp_Town_is_castle { get; set; }
			public string Comp_Town_level { get; set; }
			public string Comp_Town_background_crop_position { get; set; }
			public string Comp_Town_background_mesh { get; set; }
			public string Comp_Town_wait_mesh { get; set; }
			public string Comp_Town_gate_rotation { get; set; }
			//Village
			//public string Comp_village_id { get; set; }
			public string Comp_Village_village_type { get; set; }
			public string Comp_Village_gate_rotation { get; set; }
			public string Comp_Village_hearth { get; set; }
			public string Comp_Village_trade_bound { get; set; }
			public string Comp_Village_bound { get; set; }
			public string Comp_Village_background_crop_position { get; set; }
			public string Comp_Village_background_mesh { get; set; }
			public string Comp_Village_castle_background_mesh { get; set; }
			public string Comp_Village_wait_mesh { get; set; }
			//Hideout
			public string Comp_Hideout_id { get; set; }
			public string Comp_Hideout_map_icon { get; set; }
			public string Comp_Hideout_scene_name { get; set; }
			public string Comp_Hideout_background_crop_position { get; set; }
			public string Comp_Hideout_background_mesh { get; set; }
			public string Comp_Hideout_wait_mesh { get; set; }
			public string Comp_Hideout_gate_rotation { get; set; }

			//Locations
			public string Locations_complex_template { get; set; }
			//Location 0
			public string Locations_Location0_id { get; set; }
			public string Locations_Location0_scene_name { get; set; }
			public string Locations_Location0_scene_name_1 { get; set; }
			public string Locations_Location0_scene_name_2 { get; set; }
			public string Locations_Location0_scene_name_3 { get; set; }
			public string Locations_Location0_max_prosperity { get; set; }
			//Location 1
			public string Locations_Location1_id { get; set; }
			public string Locations_Location1_scene_name { get; set; }
			public string Locations_Location1_scene_name_1 { get; set; }
			public string Locations_Location1_scene_name_2 { get; set; }
			public string Locations_Location1_scene_name_3 { get; set; }
			public string Locations_Location1_max_prosperity { get; set; }
			//Location 2
			public string Locations_Location2_id { get; set; }
			public string Locations_Location2_scene_name { get; set; }
			public string Locations_Location2_scene_name_1 { get; set; }
			public string Locations_Location2_scene_name_2 { get; set; }
			public string Locations_Location2_scene_name_3 { get; set; }
			public string Locations_Location2_max_prosperity { get; set; }
			//Location 3
			public string Locations_Location3_id { get; set; }
			public string Locations_Location3_scene_name { get; set; }
			public string Locations_Location3_scene_name_1 { get; set; }
			public string Locations_Location3_scene_name_2 { get; set; }
			public string Locations_Location3_scene_name_3 { get; set; }
			public string Locations_Location3_max_prosperity { get; set; }
			//Location 4
			public string Locations_Location4_id { get; set; }
			public string Locations_Location4_scene_name { get; set; }
			public string Locations_Location4_scene_name_1 { get; set; }
			public string Locations_Location4_scene_name_2 { get; set; }
			public string Locations_Location4_scene_name_3 { get; set; }
			public string Locations_Location4_max_prosperity { get; set; }
			//Location 5
			public string Locations_Location5_id { get; set; }
			public string Locations_Location5_scene_name { get; set; }
			public string Locations_Location5_scene_name_1 { get; set; }
			public string Locations_Location5_scene_name_2 { get; set; }
			public string Locations_Location5_scene_name_3 { get; set; }
			public string Locations_Location5_max_prosperity { get; set; }
			//Location 6
			public string Locations_Location6_id { get; set; }
			public string Locations_Location6_scene_name { get; set; }
			public string Locations_Location6_scene_name_1 { get; set; }
			public string Locations_Location6_scene_name_2 { get; set; }
			public string Locations_Location6_scene_name_3 { get; set; }
			public string Locations_Location6_max_prosperity { get; set; }
			//Location 7
			public string Locations_Location7_id { get; set; }
			public string Locations_Location7_scene_name { get; set; }
			public string Locations_Location7_scene_name_1 { get; set; }
			public string Locations_Location7_scene_name_2 { get; set; }
			public string Locations_Location7_scene_name_3 { get; set; }
			public string Locations_Location7_max_prosperity { get; set; }
			//CommonAreas
			//Area 0
			public string CommonAreas_Area0_type { get; set; }
			public string CommonAreas_Area0_name { get; set; }
			//Area 1
			public string CommonAreas_Area1_type { get; set; }
			public string CommonAreas_Area1_name { get; set; }
			//Area 2
			public string CommonAreas_Area2_type { get; set; }
			public string CommonAreas_Area2_name { get; set; }

            public override bool Equals(object obj) {
				if (obj is SettlementRecord) return this.id.Equals(((SettlementRecord)obj).id);
                return base.Equals(obj);
            }

            public override int GetHashCode() {
                return HashCode.Combine(id, name);
            }

            public string GetSettlementType() {//Just in case... return other if not village in case of crash.
				if (Comp_Town_is_castle.ToLowerInvariant().Equals("true")) return "castle";
				if (Comp_Town_is_castle.ToLowerInvariant().Equals("false")) return "town";
				if (!Comp_Village_village_type.Equals("")) return "village";

				return "other";
            }
			public bool auto_assigned = false;
			public int auto_position_counter = 0;
        }

		public static void heroes_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			HeroRecord record = new HeroRecord();
			IEnumerable<HeroRecord> records = csv.EnumerateRecords(record);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Heroes");
				foreach (HeroRecord heroRecord in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Hero");

					//Changes
					record.is_noble = record.is_noble.ToLower();
					record.alive = record.alive.ToLower();

					//Temporary

					//Write
					writer.WriteAttributeString(null, "id", null, heroRecord.id);
					if (!heroRecord.is_noble.Equals("")) writer.WriteAttributeString(null, "is_noble", null, heroRecord.is_noble);
					if (!heroRecord.father.Equals("")) writer.WriteAttributeString(null, "father", null, heroRecord.father);
					if (!heroRecord.mother.Equals("")) writer.WriteAttributeString(null, "mother", null, heroRecord.mother);
					writer.WriteAttributeString(null, "faction", null, "Faction." + heroRecord.faction);
					if (!heroRecord.banner_key.Equals("")) writer.WriteAttributeString(null, "banner_key", null, heroRecord.banner_key);
					if (!heroRecord.spouse.Equals("")) writer.WriteAttributeString(null, "spouse", null, heroRecord.spouse);
					if (!heroRecord.alive.Equals("")) writer.WriteAttributeString(null, "alive", null, heroRecord.alive);
					if (!heroRecord.text.Equals("")) writer.WriteAttributeString(null, "text", null, heroRecord.text);

					if (!heroRecord.text.Equals("")) writeLocalizationNode(localizationWriter, "Heros.Hero." + record.id + ".text", record.text);

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class HeroRecord {
			public string id { get; set; }
			public string is_noble { get; set; }
			public string father { get; set; }
			public string mother { get; set; }
			public string faction { get; set; }
			public string banner_key { get; set; }
			public string spouse { get; set; }
			public string alive { get; set; }
			public string text { get; set; }

		}

		public static void NPCCharacters_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			NPCCharacterRecord npcCharacterRecord = new NPCCharacterRecord();
			IEnumerable<NPCCharacterRecord> records = csv.EnumerateRecords(npcCharacterRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("NPCCharacters");
				foreach (NPCCharacterRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("NPCCharacter");

					//Changes
					record.is_basic_troop = record.is_basic_troop.ToLower();
					record.is_child_template = record.is_child_template.ToLower();
					record.is_companion = record.is_companion.ToLower();
					record.is_female = record.is_female.ToLower();
					record.is_hero = record.is_hero.ToLower();
					record.is_mercenary = record.is_mercenary.ToLower();
					record.is_template = record.is_template.ToLower();

					//Defaults
					if (record.default_group.Equals("")) record.default_group = "Infantry";
					if (record.civilianTemplate.Equals("")) record.civilianTemplate = "npc_armed_wanderer_equipment_template_empire";
					if (record.battleTemplate.Equals("")) record.battleTemplate = "emp_bat_template_lady";
					if (record.level.Equals("")) record.level = "15";

					//Temporary
					if (record.age.Equals("")) record.age = "23";

					//Write
					writer.WriteAttributeString(null, "id", null, record.id);
					writer.WriteAttributeString(null, "default_group", null, record.default_group);
					writer.WriteAttributeString(null, "level", null, record.level);
					if (!record.age.Equals("")) writer.WriteAttributeString(null, "age", null, record.age);////
					if (!record.voice.Equals("")) writer.WriteAttributeString(null, "voice", null, record.voice);//
					writer.WriteAttributeString(null, "is_hero", null, record.is_hero);
					if (!record.is_female.Equals("")) writer.WriteAttributeString(null, "is_female", null, record.is_female);////
					if (!record.is_basic_troop.Equals("")) writer.WriteAttributeString(null, "is_basic_troop", null, record.is_basic_troop);//
					if (!record.is_template.Equals("")) writer.WriteAttributeString(null, "is_template", null, record.is_template);//
					if (!record.is_child_template.Equals("")) writer.WriteAttributeString(null, "is_child_template", null, record.is_child_template);//
					writer.WriteAttributeString(null, "culture", null, "Culture." + record.culture);
					writer.WriteAttributeString(null, "name", null, "{=NPCCharacters.NPCCharacter." + record.id + ".name}"+record.name);

					writeLocalizationNode(localizationWriter, "NPCCharacters.NPCCharacter." + record.id + ".name", record.name);

					if (!record.banner_symbol_mesh_name.Equals("")) writer.WriteAttributeString(null, "banner_symbol_mesh_name", null, record.banner_symbol_mesh_name);////
					if (!record.banner_symbol_color.Equals("")) writer.WriteAttributeString(null, "banner_symbol_color", null, record.banner_symbol_color);//
					if (!record.banner_key.Equals("")) writer.WriteAttributeString(null, "banner_key", null, record.banner_key);////
					if (!record.occupation.Equals("")) writer.WriteAttributeString(null, "occupation", null, record.occupation);////
					if (!record.civilianTemplate.Equals("")) writer.WriteAttributeString(null, "civilianTemplate", null, "NPCCharacter." + record.civilianTemplate);//
					if (!record.battleTemplate.Equals("")) writer.WriteAttributeString(null, "battleTemplate", null, "NPCCharacter." + record.battleTemplate);//
					if (!record.is_companion.Equals("")) writer.WriteAttributeString(null, "is_companion", null, record.is_companion);////
					if (!record.offset.Equals("")) writer.WriteAttributeString(null, "offset", null, record.offset);////
					if (!record.is_mercenary.Equals("")) writer.WriteAttributeString(null, "is_mercenary", null, record.is_mercenary);////
					if (!record.formation_position_preference.Equals("")) writer.WriteAttributeString(null, "formation_position_preference", null, record.formation_position_preference);////
					if (!record.default_equipment_set.Equals("")) writer.WriteAttributeString(null, "default_equipment_set", null, record.default_equipment_set);//
					if (!record.skill_template.Equals("")) writer.WriteAttributeString(null, "skill_template", null, "NPCCharacter."+record.skill_template);//
					if (!record.upgrade_requires.Equals("")) writer.WriteAttributeString(null, "upgrade_requires", null, record.upgrade_requires);//

					//Face
					if (!record.Face_face_key_value.Equals("") || !record.Face_face_key_template_value.Equals("") || !record.Face_BodyProperties_version.Equals("") || 
						!record.Face_hair_tags_hair_tag0_name.Equals("") || !record.Face_hair_tags_hair_tag1_name.Equals("") || !record.Face_hair_tags_hair_tag2_name.Equals("") ||
						!record.Face_beard_tags_beard_tag0_name.Equals("") || !record.Face_beard_tags_beard_tag1_name.Equals("") || !record.Face_beard_tags_beard_tag2_name.Equals("") ||
						!record.Face_tattoo_tags_tattoo_tag0_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag1_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
						writer.WriteStartElement("face");

						if (!record.Face_face_key_value.Equals("")) {
							writer.WriteStartElement("face_key");

							writer.WriteAttributeString("value",record.Face_face_key_value);
							if(!record.Face_face_key_max_value.Equals("")) writer.WriteAttributeString("max_value", record.Face_face_key_max_value);

							writer.WriteEndElement();
						}

						if (!record.Face_face_key_template_value.Equals("")) {
							writer.WriteStartElement("face_key_template");
							writer.WriteAttributeString("value", "NPCCharacter." + record.Face_face_key_template_value);
							writer.WriteEndElement();
						}

						if (!record.Face_BodyProperties_version.Equals("")) {
							writer.WriteStartElement("BodyProperties");

							writer.WriteAttributeString("version", record.Face_BodyProperties_version);
							writer.WriteAttributeString("key", record.Face_BodyProperties_key);
							if (!record.Face_BodyProperties_age.Equals("")) writer.WriteAttributeString("age", record.Face_BodyProperties_age);
							if (!record.Face_BodyProperties_weight.Equals("")) writer.WriteAttributeString("weight", record.Face_BodyProperties_weight);
							if (!record.Face_BodyProperties_build.Equals("")) writer.WriteAttributeString("build", record.Face_BodyProperties_build);

							writer.WriteEndElement();

							if(!record.Face_BodyPropertiesMax_version.Equals("")) {
								writer.WriteStartElement("BodyPropertiesMax");

								writer.WriteAttributeString("version", record.Face_BodyPropertiesMax_version);
								writer.WriteAttributeString("key", record.Face_BodyPropertiesMax_key);
								if (!record.Face_BodyPropertiesMax_age.Equals("")) writer.WriteAttributeString("age", record.Face_BodyPropertiesMax_age);
								if (!record.Face_BodyPropertiesMax_weight.Equals("")) writer.WriteAttributeString("weight", record.Face_BodyPropertiesMax_weight);
								if (!record.Face_BodyPropertiesMax_build.Equals("")) writer.WriteAttributeString("build", record.Face_BodyPropertiesMax_build);

								writer.WriteEndElement();
							}
						}

						if (!record.Face_hair_tags_hair_tag0_name.Equals("") || !record.Face_hair_tags_hair_tag1_name.Equals("") || !record.Face_hair_tags_hair_tag2_name.Equals("")) {
							writer.WriteStartElement("hair_tags");

							if(!record.Face_hair_tags_hair_tag0_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_hair_tags_hair_tag1_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_hair_tags_hair_tag2_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}
						if (!record.Face_beard_tags_beard_tag0_name.Equals("") || !record.Face_beard_tags_beard_tag1_name.Equals("") || !record.Face_beard_tags_beard_tag2_name.Equals("")) {
							writer.WriteStartElement("beard_tags");

							if (!record.Face_beard_tags_beard_tag0_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_beard_tags_beard_tag1_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_beard_tags_beard_tag2_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}
						if (!record.Face_tattoo_tags_tattoo_tag0_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag1_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
							writer.WriteStartElement("tattoo_tags");

							if (!record.Face_tattoo_tags_tattoo_tag0_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_tattoo_tags_tattoo_tag1_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					//Companion
					if (record.is_companion.Equals("true", StringComparison.OrdinalIgnoreCase)) {
						writer.WriteStartElement("Components");
						writer.WriteStartElement("Companion");
						writer.WriteAttributeString(null, "id", null, record.id.Replace("companion_", "companion_comp_"));
						writer.WriteEndElement();
						writer.WriteEndElement();
					}

					//Equipment Set - Is this actually needed?
					writer.WriteStartElement("equipmentSet");
					writer.WriteEndElement();

					if (!record.upgrade_targets_upgrade_target0_id.Equals("") || !record.upgrade_targets_upgrade_target1_id.Equals("") ||
						!record.upgrade_targets_upgrade_target2_id.Equals("") || !record.upgrade_targets_upgrade_target3_id.Equals("")) {
						writer.WriteStartElement("upgrade_targets");

						if (!record.upgrade_targets_upgrade_target0_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target0_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target1_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target1_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target2_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target2_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target3_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target3_id);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		} //Unfinished
		public class NPCCharacterRecord {
			public string id { get; set; }
			public string default_group { get; set; }
			public string voice { get; set; }
			public string is_hero { get; set; }
			public string is_female { get; set; }
			public string is_basic_troop { get; set; }
			public string is_template { get; set; }
			public string is_child_template { get; set; }
			public string culture { get; set; }
			public string name { get; set; }
			public string banner_symbol_mesh_name { get; set; }
			public string banner_symbol_color { get; set; }
			public string banner_key { get; set; }
			public string occupation { get; set; }
			public string civilianTemplate { get; set; }
			public string battleTemplate { get; set; }
			public string is_companion { get; set; }
			public string offset { get; set; }
			public string level { get; set; }
			public string age { get; set; }
			public string is_mercenary { get; set; }
			public string formation_position_preference { get; set; }
			public string default_equipment_set { get; set; }
			public string skill_template { get; set; }
			public string upgrade_requires { get; set; }

			public string upgrade_targets_upgrade_target0_id { get; set; }
			public string upgrade_targets_upgrade_target1_id { get; set; }
			public string upgrade_targets_upgrade_target2_id { get; set; }
			public string upgrade_targets_upgrade_target3_id { get; set; }

			//Face - face_key
			public string Face_face_key_value { get; set; }
			public string Face_face_key_max_value { get; set; }
			public string Face_face_key_template_value { get; set; }
			//Face - BodyProperties
			public string Face_BodyProperties_version { get; set; }
			public string Face_BodyProperties_key { get; set; }
			public string Face_BodyProperties_age { get; set; }
			public string Face_BodyProperties_weight { get; set; }
			public string Face_BodyProperties_build { get; set; }
			//Face - BodyPropertiesMax
			public string Face_BodyPropertiesMax_version { get; set; }
			public string Face_BodyPropertiesMax_key { get; set; }
			public string Face_BodyPropertiesMax_age { get; set; }
			public string Face_BodyPropertiesMax_weight { get; set; }
			public string Face_BodyPropertiesMax_build { get; set; }
			//Face - hair_tags
			public string Face_hair_tags_hair_tag0_name { get; set; }
			public string Face_hair_tags_hair_tag1_name { get; set; }
			public string Face_hair_tags_hair_tag2_name { get; set; }
			//Face - beard_tags
			public string Face_beard_tags_beard_tag0_name { get; set; }
			public string Face_beard_tags_beard_tag1_name { get; set; }
			public string Face_beard_tags_beard_tag2_name { get; set; }
			//Face - tattoo_tags
			public string Face_tattoo_tags_tattoo_tag0_name { get; set; }
			public string Face_tattoo_tags_tattoo_tag1_name { get; set; }
			public string Face_tattoo_tags_tattoo_tag2_name { get; set; }
		}

		public static void Kingdoms_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			KingdomRecord kingdomsRecord = new KingdomRecord();
			IEnumerable<KingdomRecord> records = csv.EnumerateRecords(kingdomsRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Kingdoms");
				foreach (KingdomRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Kingdom");

					//Changes

					//Defaults

					//Temporary

					//Write
					writer.WriteAttributeString("id", record.id);
					if (!record.banner_key.Equals("")) writer.WriteAttributeString("banner_key", record.banner_key);
					if (!record.owner.Equals("")) writer.WriteAttributeString("owner", "Hero." + record.owner);
					if (!record.label_color.Equals("")) writer.WriteAttributeString("label_color", record.label_color);
					writer.WriteAttributeString("primary_banner_color", record.primary_banner_color);
					writer.WriteAttributeString("secondary_banner_color", record.secondary_banner_color);
					if (!record.color.Equals("")) writer.WriteAttributeString("color", record.color);
					if (!record.color2.Equals("")) writer.WriteAttributeString("color2", record.color2);
					if (!record.alternative_color.Equals("")) writer.WriteAttributeString("alternative_color", record.alternative_color);
					if (!record.alternative_color2.Equals("")) writer.WriteAttributeString("alternative_color2", record.alternative_color2);
					writer.WriteAttributeString("culture", "Culture." + record.culture);
					if (!record.settlement_banner_mesh.Equals("")) writer.WriteAttributeString("settlement_banner_mesh", record.settlement_banner_mesh);
					if (!record.flag_mesh.Equals("")) writer.WriteAttributeString("flag_mesh", record.flag_mesh);
					writer.WriteAttributeString("name", "{=Kingdoms.Kingdom." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".name", record.name);
					//writeLocalizationNode(module_strings_writer, "str_faction_ruler." + record.id, "{=Kingdoms.Kingdom." + record.id + ".name}" + record.name);

					if (!record.short_name.Equals("")) {
						writer.WriteAttributeString("short_name", "{=Kingdoms.Kingdom." + record.id + ".short_name}" + record.short_name);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".short_name", record.short_name);
						//writeLocalizationNode(module_strings_writer, "."+record.id, "{=Kingdoms.Kingdom." + record.id + ".short_name}" + record.short_name);
					}

					if (!record.text.Equals("")) {
						writer.WriteAttributeString("text", "{=Kingdoms.Kingdom." + record.id + ".text}" + record.text);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".text", record.text);
						//writeLocalizationNode(module_strings_writer, "." + record.id, "{=Kingdoms.Kingdom." + record.id + ".text}" + record.text);
					}
					if (!record.title.Equals("")) {
						writer.WriteAttributeString("title", "{=Kingdoms.Kingdom." + record.id + ".title}" + record.title);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".title", record.title);
						//writeLocalizationNode(module_strings_writer, "." + record.id, "{=Kingdoms.Kingdom." + record.id + ".title}" + record.title);
					}
					if (!record.ruler_title.Equals("")) {
						writer.WriteAttributeString("ruler_title", "{=Kingdoms.Kingdom." + record.id + ".ruler_title}" + record.ruler_title);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".ruler_title", record.ruler_title);
						//writeLocalizationNode(module_strings_writer, "." + record.id, "{=Kingdoms.Kingdom." + record.id + ".ruler_title}" + record.ruler_title);
					}

					writer.WriteStartElement("relationships");
					if (!record.Relationships_Kingdom0_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom0_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom0_value);
						if (!record.Relationships_Kingdom0_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom0_value.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom1_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom1_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom1_value);
						if (!record.Relationships_Kingdom1_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom1_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom2_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom2_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom2_value);
						if (!record.Relationships_Kingdom2_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom2_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom3_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom3_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom3_value);
						if (!record.Relationships_Kingdom3_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom3_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					//~~~
					if (!record.Relationships_Clan0_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan0_name);
						writer.WriteAttributeString("value", record.Relationships_Clan0_value);
						if (!record.Relationships_Clan0_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan0_value.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan1_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan1_name);
						writer.WriteAttributeString("value", record.Relationships_Clan1_value);
						if (!record.Relationships_Clan1_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan1_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan2_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan2_name);
						writer.WriteAttributeString("value", record.Relationships_Clan2_value);
						if (!record.Relationships_Clan2_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan2_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan3_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan3_name);
						writer.WriteAttributeString("value", record.Relationships_Clan3_value);
						if (!record.Relationships_Clan3_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan3_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					writer.WriteEndElement();

					writer.WriteStartElement("policies");
					if (!record.Policy0.Equals("") || !record.Policy1.Equals("") || !record.Policy2.Equals("") || !record.Policy3.Equals("")) {
						if (!record.Policy0.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy0);
							writer.WriteEndElement();
						}
						if (!record.Policy1.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy1);
							writer.WriteEndElement();
						}
						if (!record.Policy2.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy2);
							writer.WriteEndElement();
						}
						if (!record.Policy3.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy3);
							writer.WriteEndElement();
						}
					}
					writer.WriteEndElement();

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class KingdomRecord {
			public string id { get; set; }
			public string banner_key { get; set; }
			public string owner { get; set; }
			public string label_color { get; set; }
			public string primary_banner_color { get; set; }
			public string secondary_banner_color { get; set; }
			public string color { get; set; }
			public string color2 { get; set; }
			public string alternative_color { get; set; }
			public string alternative_color2 { get; set; }
			public string culture { get; set; }
			public string settlement_banner_mesh { get; set; }
			public string flag_mesh { get; set; }
			public string name { get; set; }
			public string short_name { get; set; }
			public string text { get; set; }
			public string title { get; set; }
			public string ruler_title { get; set; }

			public string Policy0 { get; set; }
			public string Policy1 { get; set; }
			public string Policy2 { get; set; }
			public string Policy3 { get; set; }

			public string Relationships_Kingdom0_name { get; set; }
			public string Relationships_Kingdom0_value { get; set; }
			public string Relationships_Kingdom0_isAtWar { get; set; }
			public string Relationships_Kingdom1_name { get; set; }
			public string Relationships_Kingdom1_value { get; set; }
			public string Relationships_Kingdom1_isAtWar { get; set; }
			public string Relationships_Kingdom2_name { get; set; }
			public string Relationships_Kingdom2_value { get; set; }
			public string Relationships_Kingdom2_isAtWar { get; set; }
			public string Relationships_Kingdom3_name { get; set; }
			public string Relationships_Kingdom3_value { get; set; }
			public string Relationships_Kingdom3_isAtWar { get; set; }

			public string Relationships_Clan0_name { get; set; }
			public string Relationships_Clan0_value { get; set; }
			public string Relationships_Clan0_isAtWar { get; set; }
			public string Relationships_Clan1_name { get; set; }
			public string Relationships_Clan1_value { get; set; }
			public string Relationships_Clan1_isAtWar { get; set; }
			public string Relationships_Clan2_name { get; set; }
			public string Relationships_Clan2_value { get; set; }
			public string Relationships_Clan2_isAtWar { get; set; }
			public string Relationships_Clan3_name { get; set; }
			public string Relationships_Clan3_value { get; set; }
			public string Relationships_Clan3_isAtWar { get; set; }

		}

		public static void Clans_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			ClanRecord kingdomsRecord = new ClanRecord();
			IEnumerable<ClanRecord> records = csv.EnumerateRecords(kingdomsRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Factions");
				foreach (ClanRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Faction");

					//Changes
					record.is_bandit = record.is_bandit.ToLower();
					record.is_clan_type_mercenary = record.is_clan_type_mercenary.ToLower();
					record.is_mafia = record.is_mafia.ToLower();
					record.is_minor_faction = record.is_minor_faction.ToLower();
					record.is_nomad = record.is_nomad.ToLower();
					record.is_outlaw = record.is_outlaw.ToLower();
					record.is_sect = record.is_sect.ToLower();

					//Defaults
					if (record.tier.Equals("")) record.tier = "4";

					//Temporary

					//Write
					writer.WriteAttributeString("id", record.id);
					writer.WriteAttributeString("tier", record.tier);
					if (!record.owner.Equals("")) writer.WriteAttributeString("owner", "Hero." + record.owner);
					if (!record.label_color.Equals("")) writer.WriteAttributeString("label_color", record.label_color);
					if (!record.color.Equals("")) writer.WriteAttributeString("color", record.color);
					if (!record.color2.Equals("")) writer.WriteAttributeString("color2", record.color2);
					if (!record.alternative_color.Equals("")) writer.WriteAttributeString("alternative_color", record.alternative_color);
					if (!record.alternative_color2.Equals("")) writer.WriteAttributeString("alternative_color2", record.alternative_color2);
					if (!record.culture.Equals("")) writer.WriteAttributeString("culture", "Culture." + record.culture);
					if (!record.super_faction.Equals("")) writer.WriteAttributeString("super_faction", "Kingdom." + record.super_faction);
					if (!record.default_party_template.Equals("")) writer.WriteAttributeString("default_party_template", record.default_party_template);
					if (!record.is_bandit.Equals("")) writer.WriteAttributeString("is_bandit", record.is_bandit);
					if (!record.is_minor_faction.Equals("")) writer.WriteAttributeString("is_minor_faction", record.is_minor_faction);
					if (!record.is_outlaw.Equals("")) writer.WriteAttributeString("is_outlaw", record.is_outlaw);
					if (!record.is_clan_type_mercenary.Equals("")) writer.WriteAttributeString("is_clan_type_mercenary", record.is_clan_type_mercenary);
					if (!record.is_nomad.Equals("")) writer.WriteAttributeString("is_nomad", record.is_nomad);
					if (!record.is_sect.Equals("")) writer.WriteAttributeString("is_sect", record.is_sect);
					if (!record.is_mafia.Equals("")) writer.WriteAttributeString("is_mafia", record.is_mafia);
					if (!record.settlement_banner_mesh.Equals("")) writer.WriteAttributeString("settlement_banner_mesh", record.settlement_banner_mesh);
					if (!record.flag_mesh.Equals("")) writer.WriteAttributeString("flag_mesh", record.flag_mesh);
					writer.WriteAttributeString("name", "{=Factions.Faction." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Factions.Faction." + record.id + ".name", record.name);

					if (!record.short_name.Equals("")) writer.WriteAttributeString("short_name", "{=Factions.Faction." + record.id + ".short_name}" + record.short_name);

					if (!record.short_name.Equals("")) writeLocalizationNode(localizationWriter, "Factions.Faction." + record.id + ".short_name", record.short_name);

					if (!record.text.Equals("")) writer.WriteAttributeString("text", "{=Factions.Faction." + record.id + ".text}" + record.text);

					if (!record.text.Equals("")) writeLocalizationNode(localizationWriter, "Factions.Faction." + record.id + ".text", record.text);


					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class ClanRecord {
			public string id { get; set; }
			public string tier { get; set; }
			public string owner { get; set; }
			public string label_color { get; set; }
			public string color { get; set; }
			public string color2 { get; set; }
			public string alternative_color { get; set; }
			public string alternative_color2 { get; set; }
			public string culture { get; set; }
			public string super_faction { get; set; }
			public string default_party_template { get; set; }
			public string is_bandit { get; set; }
			public string is_minor_faction { get; set; }
			public string is_outlaw { get; set; }
			public string is_clan_type_mercenary { get; set; }
			public string is_nomad { get; set; }
			public string is_sect { get; set; }
			public string is_mafia { get; set; }
			public string settlement_banner_mesh { get; set; }
			public string flag_mesh { get; set; }
			public string name { get; set; }
			public string short_name { get; set; }
			public string text { get; set; }
		}

		public static void Cultures_CSVtoXML (string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			CultureRecord cultureRecord = new CultureRecord();
			IEnumerable<CultureRecord> records = csv.EnumerateRecords(cultureRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("SPCultures");
				foreach (CultureRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Culture");

					//Changes
					record.is_main_culture = record.is_main_culture.ToLower();
					record.is_bandit = record.is_bandit.ToLower();
					record.can_have_settlement = record.can_have_settlement.ToLower();

					//Defaults
					if (record.default_party_template.Equals("")) record.default_party_template = "kingdom_hero_party_empire_template";
					if (record.villager_party_template.Equals("")) record.villager_party_template = "kingdom_hero_party_empire_template";
					if (record.elite_caravan_party_template.Equals("")) record.elite_caravan_party_template = "kingdom_hero_party_empire_template";
					if (record.bandit_boss_party_template.Equals("")) record.bandit_boss_party_template = "kingdom_hero_party_empire_template";
					if (record.caravan_party_template.Equals("")) record.caravan_party_template = "kingdom_hero_party_empire_template";
					if (record.militia_party_template.Equals("")) record.militia_party_template = "kingdom_hero_party_empire_template";
					if (record.rebels_party_template.Equals("")) record.rebels_party_template = "kingdom_hero_party_empire_template";

					if (record.melee_militia_troop.Equals("")) record.melee_militia_troop = record.basic_troop;
					if (record.melee_elite_militia_troop.Equals("")) record.melee_elite_militia_troop = record.elite_basic_troop;
					if (record.ranged_militia_troop.Equals("")) record.ranged_militia_troop = record.basic_troop;
					if (record.ranged_elite_militia_troop.Equals("")) record.ranged_elite_militia_troop = record.elite_basic_troop;

					if (record.tournament_master.Equals("")) record.tournament_master = record.elite_basic_troop;
					if (record.caravan_master.Equals("")) record.caravan_master = record.elite_basic_troop;
					if (record.armed_trader.Equals("")) record.armed_trader = record.elite_basic_troop;
					if (record.caravan_guard.Equals("")) record.caravan_guard = record.elite_basic_troop;
					if (record.veteran_caravan_guard.Equals("")) record.veteran_caravan_guard = record.elite_basic_troop;
					if (record.duel_preset.Equals("")) record.duel_preset = record.elite_basic_troop;
					if (record.prison_guard.Equals("")) record.prison_guard = record.elite_basic_troop;
					if (record.guard.Equals("")) record.guard = record.elite_basic_troop;
					if (record.steward.Equals("")) record.steward = record.elite_basic_troop;
					if (record.blacksmith.Equals("")) record.blacksmith = record.elite_basic_troop;
					if (record.weaponsmith.Equals("")) record.weaponsmith = record.elite_basic_troop;

					if (record.townswoman.Equals("")) record.townswoman = record.elite_basic_troop;
					if (record.townswoman_infant.Equals("")) record.townswoman_infant = record.elite_basic_troop;
					if (record.townswoman_child.Equals("")) record.townswoman_child = record.elite_basic_troop;
					if (record.townswoman_teenager.Equals("")) record.townswoman_teenager = record.elite_basic_troop;
					if (record.townsman.Equals("")) record.townsman = record.elite_basic_troop;
					if (record.townsman_infant.Equals("")) record.townsman_infant = record.elite_basic_troop;
					if (record.townsman_child.Equals("")) record.townsman_child = record.elite_basic_troop;
					if (record.townsman_teenager.Equals("")) record.townsman_teenager = record.elite_basic_troop;

					if (record.villager.Equals("")) record.villager = record.elite_basic_troop;
					if (record.villager_woman.Equals("")) record.villager_woman = record.elite_basic_troop;
					if (record.villager_male_child.Equals("")) record.villager_male_child = record.elite_basic_troop;
					if (record.villager_male_teenager.Equals("")) record.villager_male_teenager = record.elite_basic_troop;
					if (record.villager_female_child.Equals("")) record.villager_female_child = record.elite_basic_troop;
					if (record.villager_female_teenager.Equals("")) record.villager_female_teenager = record.elite_basic_troop;

					if (record.ransom_broker.Equals("")) record.ransom_broker = record.elite_basic_troop;
					if (record.gangleader_bodyguard.Equals("")) record.gangleader_bodyguard = record.elite_basic_troop;
					if (record.merchant_notary.Equals("")) record.merchant_notary = record.elite_basic_troop;
					if (record.preacher_notary.Equals("")) record.preacher_notary = record.elite_basic_troop;
					if (record.rural_notable_notary.Equals("")) record.rural_notable_notary = record.elite_basic_troop;
					if (record.shop_worker.Equals("")) record.shop_worker = record.elite_basic_troop;

					if (record.tavernkeeper.Equals("")) record.tavernkeeper = record.elite_basic_troop;
					if (record.taverngamehost.Equals("")) record.taverngamehost = record.elite_basic_troop;
					if (record.musician.Equals("")) record.musician = record.elite_basic_troop;
					if (record.tavern_wench.Equals("")) record.tavern_wench = record.elite_basic_troop;

					if (record.armorer.Equals("")) record.armorer = record.elite_basic_troop;
					if (record.horseMerchant.Equals("")) record.horseMerchant = record.elite_basic_troop;
					if (record.barber.Equals("")) record.barber = record.elite_basic_troop;
					if (record.merchant.Equals("")) record.merchant = record.elite_basic_troop;
					if (record.beggar.Equals("")) record.beggar = record.elite_basic_troop;
					if (record.female_beggar.Equals("")) record.female_beggar = record.elite_basic_troop;
					if (record.female_dancer.Equals("")) record.female_dancer = record.elite_basic_troop;

					if (record.gear_practice_dummy.Equals("")) record.gear_practice_dummy = record.elite_basic_troop;
					if (record.weapon_practice_stage_1.Equals("")) record.weapon_practice_stage_1 = record.elite_basic_troop;
					if (record.weapon_practice_stage_2.Equals("")) record.weapon_practice_stage_2 = record.elite_basic_troop;
					if (record.weapon_practice_stage_3.Equals("")) record.weapon_practice_stage_3 = record.elite_basic_troop;

					if (record.gear_dummy.Equals("")) record.gear_dummy = record.elite_basic_troop;
					if (record.bandit_bandit.Equals("")) record.bandit_bandit = record.elite_basic_troop;
					if (record.bandit_chief.Equals("")) record.bandit_chief = record.elite_basic_troop;
					if (record.bandit_raider.Equals("")) record.bandit_raider = record.elite_basic_troop;
					if (record.bandit_boss.Equals("")) record.bandit_boss = record.elite_basic_troop;
					//if (record.board_game_type.Equals("")) record.board_game_type = "";
					/*
					if (record.tournament_template_one_participant_set_v1.Equals("")) record.tournament_template_one_participant_set_v1 = record.elite_basic_troop;
					if (record.tournament_template_one_participant_set_v2.Equals("")) record.tournament_template_one_participant_set_v2 = record.elite_basic_troop;
					if (record.tournament_template_one_participant_set_v3.Equals("")) record.tournament_template_one_participant_set_v3 = record.elite_basic_troop;
					if (record.tournament_template_one_participant_set_v4.Equals("")) record.tournament_template_one_participant_set_v4 = record.elite_basic_troop;

					if (record.tournament_template_two_participant_set_v1.Equals("")) record.tournament_template_two_participant_set_v1 = record.elite_basic_troop;
					if (record.tournament_template_two_participant_set_v2.Equals("")) record.tournament_template_two_participant_set_v2 = record.elite_basic_troop;
					if (record.tournament_template_two_participant_set_v3.Equals("")) record.tournament_template_two_participant_set_v3 = record.elite_basic_troop;
					if (record.tournament_template_two_participant_set_v4.Equals("")) record.tournament_template_two_participant_set_v4 = record.elite_basic_troop;

					if (record.tournament_template_four_participant_set_v1.Equals("")) record.tournament_template_four_participant_set_v1 = record.elite_basic_troop;
					if (record.tournament_template_four_participant_set_v2.Equals("")) record.tournament_template_four_participant_set_v2 = record.elite_basic_troop;
					if (record.tournament_template_four_participant_set_v3.Equals("")) record.tournament_template_four_participant_set_v3 = record.elite_basic_troop;
					if (record.tournament_template_four_participant_set_v4.Equals("")) record.tournament_template_four_participant_set_v4 = record.elite_basic_troop;
					*/

					//Temporary


					//Write
					writer.WriteAttributeString("id", record.id);
					writer.WriteAttributeString("name", "{=Cultures.Culture." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".name", record.name);

					if (!record.is_main_culture.Equals("")) writer.WriteAttributeString("is_main_culture", record.is_main_culture);
					if (!record.can_have_settlement.Equals("")) writer.WriteAttributeString("can_have_settlement", record.can_have_settlement);
					if (!record.town_edge_number.Equals("")) writer.WriteAttributeString("town_edge_number", record.town_edge_number);
					if (!record.militia_bonus.Equals("")) writer.WriteAttributeString("militia_bonus", record.militia_bonus);
					if (!record.prosperity_bonus.Equals("")) writer.WriteAttributeString("prosperity_bonus", record.prosperity_bonus);
					if (!record.encounter_background_mesh.Equals("")) writer.WriteAttributeString("encounter_background_mesh", record.encounter_background_mesh);
					writer.WriteAttributeString("basic_troop", "NPCCharacter." + record.basic_troop);
					writer.WriteAttributeString("elite_basic_troop", "NPCCharacter." + record.elite_basic_troop);

					if (!record.is_bandit.Equals("")) writer.WriteAttributeString("is_bandit", record.is_bandit);
					if (!record.default_face_key.Equals("")) writer.WriteAttributeString("default_face_key", record.default_face_key);

					if (!record.default_party_template.Equals("")) writer.WriteAttributeString("default_party_template", "PartyTemplate." + record.default_party_template);
					if (!record.villager_party_template.Equals("")) writer.WriteAttributeString("villager_party_template", "PartyTemplate." + record.villager_party_template);
					if (!record.elite_caravan_party_template.Equals("")) writer.WriteAttributeString("elite_caravan_party_template", "PartyTemplate." + record.elite_caravan_party_template);
					if (!record.bandit_boss_party_template.Equals("")) writer.WriteAttributeString("bandit_boss_party_template", "PartyTemplate." + record.bandit_boss_party_template);
					if (!record.caravan_party_template.Equals("")) writer.WriteAttributeString("caravan_party_template", "PartyTemplate." + record.caravan_party_template);
					if (!record.militia_party_template.Equals("")) writer.WriteAttributeString("militia_party_template", "PartyTemplate." + record.militia_party_template);
					if (!record.rebels_party_template.Equals("")) writer.WriteAttributeString("rebels_party_template", "PartyTemplate." + record.rebels_party_template);

					if (!record.melee_militia_troop.Equals("")) writer.WriteAttributeString("melee_militia_troop", "NPCCharacter." + record.melee_militia_troop);
					if (!record.melee_elite_militia_troop.Equals("")) writer.WriteAttributeString("melee_elite_militia_troop", "NPCCharacter." + record.melee_elite_militia_troop);
					if (!record.ranged_militia_troop.Equals("")) writer.WriteAttributeString("ranged_militia_troop", "NPCCharacter." + record.ranged_militia_troop);
					if (!record.ranged_elite_militia_troop.Equals("")) writer.WriteAttributeString("ranged_elite_militia_troop", "NPCCharacter." + record.ranged_elite_militia_troop);

					if (!record.tournament_master.Equals("")) writer.WriteAttributeString("tournament_master", "NPCCharacter." + record.tournament_master);
					if (!record.caravan_master.Equals("")) writer.WriteAttributeString("caravan_master", "NPCCharacter." + record.caravan_master);
					if (!record.armed_trader.Equals("")) writer.WriteAttributeString("armed_trader", "NPCCharacter." + record.armed_trader);
					if (!record.caravan_guard.Equals("")) writer.WriteAttributeString("caravan_guard", "NPCCharacter." + record.caravan_guard);
					if (!record.veteran_caravan_guard.Equals("")) writer.WriteAttributeString("veteran_caravan_guard", "NPCCharacter." + record.veteran_caravan_guard);

					if (!record.duel_preset.Equals("")) writer.WriteAttributeString("duel_preset", "NPCCharacter." + record.duel_preset);

					if (!record.prison_guard.Equals("")) writer.WriteAttributeString("prison_guard", "NPCCharacter." + record.prison_guard);
					if (!record.guard.Equals("")) writer.WriteAttributeString("guard", "NPCCharacter." + record.guard);
					if (!record.steward.Equals("")) writer.WriteAttributeString("steward", "NPCCharacter." + record.steward);
					if (!record.blacksmith.Equals("")) writer.WriteAttributeString("blacksmith", "NPCCharacter." + record.blacksmith);
					if (!record.weaponsmith.Equals("")) writer.WriteAttributeString("weaponsmith", "NPCCharacter." + record.weaponsmith);

					if (!record.townswoman.Equals("")) writer.WriteAttributeString("townswoman", "NPCCharacter." + record.townswoman);
					if (!record.townswoman_infant.Equals("")) writer.WriteAttributeString("townswoman_infant", "NPCCharacter." + record.townswoman_infant);
					if (!record.townswoman_child.Equals("")) writer.WriteAttributeString("townswoman_child", "NPCCharacter." + record.townswoman_child);
					if (!record.townswoman_teenager.Equals("")) writer.WriteAttributeString("townswoman_teenager", "NPCCharacter." + record.townswoman_teenager);
					if (!record.townsman.Equals("")) writer.WriteAttributeString("townsman", "NPCCharacter." + record.townsman);
					if (!record.townsman_infant.Equals("")) writer.WriteAttributeString("townsman_infant", "NPCCharacter." + record.townsman_infant);
					if (!record.townsman_child.Equals("")) writer.WriteAttributeString("townsman_child", "NPCCharacter." + record.townsman_child);
					if (!record.townsman_teenager.Equals("")) writer.WriteAttributeString("townsman_teenager", "NPCCharacter." + record.townsman_teenager);

					if (!record.villager.Equals("")) writer.WriteAttributeString("villager", "NPCCharacter." + record.villager);
					if (!record.villager_woman.Equals("")) writer.WriteAttributeString("villager_woman", "NPCCharacter." + record.villager_woman);
					if (!record.villager_male_child.Equals("")) writer.WriteAttributeString("villager_male_child", "NPCCharacter." + record.villager_male_child);
					if (!record.villager_male_teenager.Equals("")) writer.WriteAttributeString("villager_male_teenager", "NPCCharacter." + record.villager_male_teenager);
					if (!record.villager_female_child.Equals("")) writer.WriteAttributeString("villager_female_child", "NPCCharacter." + record.villager_female_child);
					if (!record.villager_female_teenager.Equals("")) writer.WriteAttributeString("villager_female_teenager", "NPCCharacter." + record.villager_female_teenager);

					if (!record.ransom_broker.Equals("")) writer.WriteAttributeString("ransom_broker", "NPCCharacter." + record.ransom_broker);

					if (!record.gangleader_bodyguard.Equals("")) writer.WriteAttributeString("gangleader_bodyguard", "NPCCharacter." + record.gangleader_bodyguard);

					if (!record.merchant_notary.Equals("")) writer.WriteAttributeString("merchant_notary", "NPCCharacter." + record.merchant_notary);
					if (!record.preacher_notary.Equals("")) writer.WriteAttributeString("preacher_notary", "NPCCharacter." + record.preacher_notary);
					if (!record.rural_notable_notary.Equals("")) writer.WriteAttributeString("rural_notable_notary", "NPCCharacter." + record.rural_notable_notary);

					if (!record.shop_worker.Equals("")) writer.WriteAttributeString("shop_worker", "NPCCharacter." + record.shop_worker);

					if (!record.tavernkeeper.Equals("")) writer.WriteAttributeString("tavernkeeper", "NPCCharacter." + record.tavernkeeper);
					if (!record.taverngamehost.Equals("")) writer.WriteAttributeString("taverngamehost", "NPCCharacter." + record.taverngamehost);
					if (!record.musician.Equals("")) writer.WriteAttributeString("musician", "NPCCharacter." + record.musician);
					if (!record.tavern_wench.Equals("")) writer.WriteAttributeString("tavern_wench", "NPCCharacter." + record.tavern_wench);

					if (!record.armorer.Equals("")) writer.WriteAttributeString("armorer", "NPCCharacter." + record.armorer);
					if (!record.horseMerchant.Equals("")) writer.WriteAttributeString("horseMerchant", "NPCCharacter." + record.horseMerchant);
					if (!record.barber.Equals("")) writer.WriteAttributeString("barber", "NPCCharacter." + record.barber);
					if (!record.merchant.Equals("")) writer.WriteAttributeString("merchant", "NPCCharacter." + record.merchant);

					if (!record.beggar.Equals("")) writer.WriteAttributeString("beggar", "NPCCharacter." + record.beggar);
					if (!record.female_beggar.Equals("")) writer.WriteAttributeString("female_beggar", "NPCCharacter." + record.female_beggar);

					if (!record.female_dancer.Equals("")) writer.WriteAttributeString("female_dancer", "NPCCharacter." + record.female_dancer);

					if (!record.gear_practice_dummy.Equals("")) writer.WriteAttributeString("gear_practice_dummy", "NPCCharacter." + record.gear_practice_dummy);
					if (!record.weapon_practice_stage_1.Equals("")) writer.WriteAttributeString("weapon_practice_stage_1", "NPCCharacter." + record.weapon_practice_stage_1);
					if (!record.weapon_practice_stage_2.Equals("")) writer.WriteAttributeString("weapon_practice_stage_2", "NPCCharacter." + record.weapon_practice_stage_2);
					if (!record.weapon_practice_stage_3.Equals("")) writer.WriteAttributeString("weapon_practice_stage_3", "NPCCharacter." + record.weapon_practice_stage_3);
					if (!record.gear_dummy.Equals("")) writer.WriteAttributeString("gear_dummy", "NPCCharacter." + record.gear_dummy);

					if (!record.bandit_bandit.Equals("")) writer.WriteAttributeString("bandit_bandit", "NPCCharacter." + record.bandit_bandit);
					if (!record.bandit_chief.Equals("")) writer.WriteAttributeString("bandit_chief", "NPCCharacter." + record.bandit_chief);
					if (!record.bandit_raider.Equals("")) writer.WriteAttributeString("bandit_raider", "NPCCharacter." + record.bandit_raider);
					if (!record.bandit_boss.Equals("")) writer.WriteAttributeString("bandit_boss", "NPCCharacter." + record.bandit_boss);

					if (!record.board_game_type.Equals("")) writer.WriteAttributeString("board_game_type", record.board_game_type);

					if (!record.male_names.Equals("")) {
						writer.WriteStartElement("male_names");

						string[] names = record.male_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Cultures.Culture." + record.id + ".male_names." + name.ToLower() + "}" + name);
							writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".male_names." + name.ToLower(), name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.female_names.Equals("")) {
						writer.WriteStartElement("female_names");

						string[] names = record.female_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Cultures.Culture." + record.id + ".female_names." + name.ToLower() + "}" + name);
							writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".female_names." + name.ToLower(), name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}
					if (!record.clan_names.Equals("")) {
						writer.WriteStartElement("clan_names");

						string[] names = record.clan_names.Split(";");

						foreach (string name in names) {
							writer.WriteStartElement("name");
							writer.WriteAttributeString("name", "{Cultures.Culture." + record.id + ".clan_names." + name.ToLower() + "}" + name);
							writeLocalizationNode(localizationWriter, "Culture.Culture." + record.id + ".clan_names." + name.ToLower(), name);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.tournament_template_two_participant_set_v1.Equals("") || !record.tournament_template_two_participant_set_v2.Equals("") || !record.tournament_template_two_participant_set_v3.Equals("") || !record.tournament_template_two_participant_set_v4.Equals("")) {
						writer.WriteStartElement("tournament_team_templates_two_participant");

						if (!record.tournament_template_two_participant_set_v1.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v1);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_two_participant_set_v2.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v2);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_two_participant_set_v3.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v3);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_two_participant_set_v4.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_two_participant_set_v4);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.tournament_template_four_participant_set_v1.Equals("") || !record.tournament_template_four_participant_set_v2.Equals("") || !record.tournament_template_four_participant_set_v3.Equals("") || !record.tournament_template_four_participant_set_v4.Equals("")) {
						writer.WriteStartElement("tournament_team_templates_four_participant");

						if (!record.tournament_template_four_participant_set_v1.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v1);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_four_participant_set_v2.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v2);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_four_participant_set_v3.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v3);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_four_participant_set_v4.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_four_participant_set_v4);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.tournament_template_one_participant_set_v1.Equals("") || !record.tournament_template_one_participant_set_v2.Equals("") || !record.tournament_template_one_participant_set_v3.Equals("") || !record.tournament_template_one_participant_set_v4.Equals("")) {
						writer.WriteStartElement("tournament_team_templates_one_participant");

						if (!record.tournament_template_one_participant_set_v1.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v1);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_one_participant_set_v2.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v2);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_one_participant_set_v3.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v3);
							writer.WriteEndElement();
						}
						if (!record.tournament_template_one_participant_set_v4.Equals("")) {
							writer.WriteStartElement("template");
							writer.WriteAttributeString("name", record.tournament_template_one_participant_set_v4);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if(!record.str_culture_description.Equals("")) { 
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_culture_description", record.str_culture_description);
						writeLocalizationNode(module_strings_writer, "str_culture_description." + record.id, "{=Cultures.Culture." + record.id + ".str_culture_description}" + record.str_culture_description);
					}
					if (!record.str_culture_rich_name.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_culture_rich_name", record.str_culture_rich_name);
						writeLocalizationNode(module_strings_writer, "str_culture_rich_name." + record.id, "{=Cultures.Culture." + record.id + ".str_culture_rich_name}" + record.str_culture_rich_name);
					}
					if (!record.str_faction_official.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_official", record.str_faction_official);
						writeLocalizationNode(module_strings_writer, "str_faction_official." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_official}" + record.str_faction_official);
					}
					if (!record.str_faction_official_f.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_official_f", record.str_faction_official_f);
						writeLocalizationNode(module_strings_writer, "str_faction_official_f." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_official_f}" + record.str_faction_official_f);
					}
					if (!record.str_faction_ruler.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler", record.str_faction_ruler);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler}" + record.str_faction_ruler);
					}
					if (!record.str_faction_ruler_f.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler_f", record.str_faction_ruler_f);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler_f." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler_f}" + record.str_faction_ruler_f);
					}
					if (!record.str_faction_ruler_term_in_speech.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler_term_in_speech", record.str_faction_ruler_term_in_speech);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler_term_in_speech." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler_term_in_speech}" + record.str_faction_ruler_term_in_speech);
					}
					if (!record.str_faction_ruler_name_with_title.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_ruler_name_with_title", record.str_faction_ruler_name_with_title);
						writeLocalizationNode(module_strings_writer, "str_faction_ruler_name_with_title." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_ruler_name_with_title}" + record.str_faction_ruler_name_with_title);
					}
					if (!record.str_faction_noble_name_with_title.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_noble_name_with_title", record.str_faction_noble_name_with_title);
						writeLocalizationNode(module_strings_writer, "str_faction_noble_name_with_title." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_noble_name_with_title}" + record.str_faction_noble_name_with_title);
					}
					if (!record.str_adjective_for_faction.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_adjective_for_faction", record.str_adjective_for_faction);
						writeLocalizationNode(module_strings_writer, "str_adjective_for_faction." + record.id, "{=Cultures.Culture." + record.id + ".str_adjective_for_faction}" + record.str_adjective_for_faction);
					}
					if (!record.str_short_term_for_faction.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_short_term_for_faction", record.str_short_term_for_faction);
						writeLocalizationNode(module_strings_writer, "str_short_term_for_faction." + record.id, "{=Cultures.Culture." + record.id + ".str_short_term_for_faction}" + record.str_short_term_for_faction);
					}
					if (!record.str_faction_formal_name_for_culture.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_faction_formal_name_for_culture", record.str_faction_formal_name_for_culture);
						writeLocalizationNode(module_strings_writer, "str_faction_formal_name_for_culture." + record.id, "{=Cultures.Culture." + record.id + ".str_faction_formal_name_for_culture}" + record.str_faction_formal_name_for_culture);
					}
					if (!record.str_neutral_term_for_culture.Equals("")) {
						writeLocalizationNode(localizationWriter, "Cultures.Culture." + record.id + ".str_neutral_term_for_culture", record.str_neutral_term_for_culture);
						writeLocalizationNode(module_strings_writer, "str_neutral_term_for_culture." + record.id, "{=Cultures.Culture." + record.id + ".str_neutral_term_for_culture}" + record.str_neutral_term_for_culture);
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class CultureRecord {
			public string id { get; set; }
			public string name { get; set; }
			public string is_main_culture { get; set; }
			public string can_have_settlement { get; set; }
			public string town_edge_number { get; set; }
			public string militia_bonus { get; set; }
			public string prosperity_bonus { get; set; }
			public string encounter_background_mesh { get; set; }
			public string basic_troop { get; set; }
			public string elite_basic_troop { get; set; }

			public string is_bandit { get; set; }
			public string default_face_key { get; set; }
			public string default_party_template { get; set; }
			public string villager_party_template { get; set; }
			public string elite_caravan_party_template { get; set; }
			public string bandit_boss_party_template { get; set; }
			public string caravan_party_template { get; set; }
			public string militia_party_template { get; set; }
			public string rebels_party_template { get; set; }

			public string melee_militia_troop { get; set; }
			public string melee_elite_militia_troop { get; set; }
			public string ranged_militia_troop { get; set; }
			public string ranged_elite_militia_troop { get; set; }
			public string tournament_master { get; set; }
			public string caravan_master { get; set; }
			public string armed_trader { get; set; }
			public string caravan_guard { get; set; }
			public string veteran_caravan_guard { get; set; }

			public string duel_preset { get; set; }
			public string prison_guard { get; set; }
			public string guard { get; set; }
			public string steward { get; set; }
			public string blacksmith { get; set; }
			public string weaponsmith { get; set; }
			public string townswoman { get; set; }
			public string townswoman_infant { get; set; }
			public string townswoman_child { get; set; }
			public string townswoman_teenager { get; set; }
			public string townsman { get; set; }
			public string townsman_infant { get; set; }
			public string townsman_child { get; set; }
			public string townsman_teenager { get; set; }
			public string villager { get; set; }
			public string villager_woman { get; set; }
			public string villager_male_child { get; set; }
			public string villager_male_teenager { get; set; }
			public string villager_female_child { get; set; }
			public string villager_female_teenager { get; set; }

			public string ransom_broker { get; set; }
			public string gangleader_bodyguard { get; set; }
			public string merchant_notary { get; set; }
			public string preacher_notary { get; set; }
			public string rural_notable_notary { get; set; }
			public string shop_worker { get; set; }
			public string tavernkeeper { get; set; }
			public string taverngamehost { get; set; }
			public string musician { get; set; }
			public string tavern_wench { get; set; }
			public string armorer { get; set; }
			public string horseMerchant { get; set; }
			public string barber { get; set; }
			public string merchant { get; set; }
			public string beggar { get; set; }
			public string female_beggar { get; set; }
			public string female_dancer { get; set; }
			public string gear_practice_dummy { get; set; }
			public string weapon_practice_stage_1 { get; set; }
			public string weapon_practice_stage_2 { get; set; }
			public string weapon_practice_stage_3 { get; set; }
			public string gear_dummy { get; set; }
			public string bandit_bandit { get; set; }
			public string bandit_chief { get; set; }
			public string bandit_raider { get; set; }
			public string bandit_boss { get; set; }
			public string board_game_type { get; set; }

			public string tournament_template_one_participant_set_v1 { get; set; }
			public string tournament_template_one_participant_set_v2 { get; set; }
			public string tournament_template_one_participant_set_v3 { get; set; }
			public string tournament_template_one_participant_set_v4 { get; set; }
			public string tournament_template_two_participant_set_v1 { get; set; }
			public string tournament_template_two_participant_set_v2 { get; set; }
			public string tournament_template_two_participant_set_v3 { get; set; }
			public string tournament_template_two_participant_set_v4 { get; set; }
			public string tournament_template_four_participant_set_v1 { get; set; }
			public string tournament_template_four_participant_set_v2 { get; set; }
			public string tournament_template_four_participant_set_v3 { get; set; }
			public string tournament_template_four_participant_set_v4 { get; set; }

			public string male_names { get; set; }
			public string female_names { get; set; }
			public string clan_names { get; set; }

			public string str_culture_description { get; set; }
			public string str_culture_rich_name { get; set; }
			public string str_faction_official { get; set; }
			public string str_faction_official_f { get; set; }
			public string str_faction_ruler { get; set; }
			public string str_faction_ruler_f { get; set; }
			public string str_faction_ruler_term_in_speech { get; set; }
			public string str_faction_ruler_name_with_title { get; set; }
			public string str_faction_noble_name_with_title { get; set; }
			public string str_adjective_for_faction { get; set; }
			public string str_short_term_for_faction { get; set; }
			public string str_faction_formal_name_for_culture { get; set; }
			public string str_neutral_term_for_culture { get; set; }

			                  
		}

		public static void writeLocalizationNode(XmlWriter writer, string id, string text) {
			writer.WriteStartElement("string");
			writer.WriteAttributeString("id", id);
			writer.WriteAttributeString("text", text);
			writer.WriteEndElement();
		}

		public static void initializeLocalizationWriter(XmlWriter localizationWriter) {
			localizationWriter.WriteStartElement("base");
			localizationWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			localizationWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			localizationWriter.WriteAttributeString("type", "string");
			localizationWriter.WriteStartElement("tags");
			localizationWriter.WriteStartElement("tag");
			localizationWriter.WriteAttributeString("language", "English");
			localizationWriter.WriteEndElement();
			localizationWriter.WriteEndElement();
			localizationWriter.WriteStartElement("strings");
		}
	}
}
