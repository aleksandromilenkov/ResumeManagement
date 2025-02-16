import React from "react";
import Box from "@mui/material/Box";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { ICompany, IJob } from "../../types/globalTypes";
type Props = {
  jobs: IJob[];
};

const Jobs = ({ jobs }: Props) => {
    const columns: GridColDef<(typeof rows)[number]>[] = [
        {
          field: "title",
          headerName: "Job Title",
          width: 150,
          editable: true,
        },
        {
          field: "level",
          headerName: "Job Level",
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
        {
          field: "isActive",
          headerName: "Is Active",
          width: 160,
        },
      ];
      const mappedJobs = jobs.map(job => {
        return {...job,
             createdAt: new Date(job.createdAt).toLocaleDateString(),
             updatedAt: new Date(job.updatedAt).toLocaleDateString(),
            }
      })
      const rows = [...mappedJobs];
  if(rows.length === 0) return <p>No Jobs found.</p>
  return (
      <Box sx={{ height: 400, width: "content-fit" }} className="jobs-grid">
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

export default Jobs;
