import React from "react";
import Box from "@mui/material/Box";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { ICompany } from "../../types/globalTypes";
type Props = {
  companies: ICompany[];
};

const Companies = ({ companies }: Props) => {
    const columns: GridColDef<(typeof rows)[number]>[] = [
        {
          field: "name",
          headerName: "Name",
          width: 150,
          editable: true,
        },
        {
          field: "size",
          headerName: "Size",
          width: 150,
          editable: true,
        },
        {
          field: "createdAt",
          headerName: "Ceated At",
          type: "string",
          width: 110,
          editable: true,
        },
        {
          field: "updatedAt",
          headerName: "Updated At",
          width: 160,
        },
      ];
      const mappedCompanies = companies.map(company => {
        return {...company,
             createdAt: new Date(company.createdAt).toLocaleDateString(),
             updatedAt: new Date(company.updatedAt).toLocaleDateString(),
            }
      })
      const rows = [...mappedCompanies];
  if(rows.length === 0) return <p>No Companies found.</p>
  return (
      <Box sx={{ height: 400, width: "content-fit" }} className="companies-grid">
        <DataGrid
          rows={rows}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: {
                pageSize: 5,
              },
            },
          }}
          pageSizeOptions={[5]}
          disableRowSelectionOnClick
        />
      </Box>
  );
};

export default Companies;
