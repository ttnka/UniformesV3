This code includes a dependency on Duende IdentityServer.
This is an open source product with a reciprocal license agreement. If you plan to use Duende IdentityServer in production this may require a license fee.
To see how to use Azure Active Directory for your identity please see https://aka.ms/aspnetidentityserver
To see if you require a commercial license for Duende IdentityServer please see https://aka.ms/identityserverlicense




var v = linea.Split(',');
                            Z260_Folio elFolio = new()
                            {
                                FolioId = Guid.NewGuid().ToString(),
                                RegId = v[0],
                                FechaEntrega = Convert.ToDateTime(v[1]),
                                Status = int.Parse(v[2]),
                                Folio = v[3],
                                NombreCompleto = v[4],
                                Curp = v[5],
                                TurnoId = v[6],
                                Grado = int.Parse(v[7]),
                                Codigo = v[8],
                                EscuelaId = v[9],
                                InscStatusId = v[10],
                                GeneroId = v[11],
                                NivelId = v[12],
                                TipoValId = v[13],
                                Localidad = v[14],
                                Municipio = v[15]
                            };
                            ResultList.Add(elFolio);
                        }
                        Titulos++;



                         MemoryStream ms = new MemoryStream();
                fileR.OpenReadStream().CopyTo(ms);
                try
                {
                    using (var lector = new StreamReader(fileR.OpenReadStream()))
                    {
                        using (var csv = new CsvReader(lector, CultureInfo.CurrentCulture))
                        {
                            var leyo = csv.Read();
                            Console.WriteLine(leyo);
                            var tit = csv.ReadHeader();
                            Console.WriteLine(tit);
                            while (csv.Read())
                            {
                                var newFolio = csv.GetRecord<Z260_Folio>();
                                newFolio.FolioId = Guid.NewGuid().ToString();
                                ResultList.Add(newFolio);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.ToString());
                    return;
                }
                