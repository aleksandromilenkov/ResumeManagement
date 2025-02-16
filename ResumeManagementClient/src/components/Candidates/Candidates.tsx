import { DataGrid, GridColDef } from "@mui/x-data-grid";
import React from "react";
import { ICandidate } from "../../types/globalTypes";
import { Box } from "@mui/material";
import "./candidates.scss";
import { baseUrl } from "../../constants/urlConstants";
import { Download, PictureAsPdf } from "@mui/icons-material";
type Props = {
  candidates: ICandidate[];
};

const Candidates = ({ candidates }: Props) => {
  const columns: GridColDef<(typeof rows)[number]>[] = [
    {
      field: "firstName",
      headerName: "First Name",
      width: 150,
      editable: true,
    },
    {
      field: "lastName",
      headerName: "Last Name",
      width: 150,
      editable: true,
    },
    {
      field: "email",
      headerName: "Email",
      width: 150,
      editable: true,
    },
    {
      field: "phone",
      headerName: "Phone Number",
      width: 150,
      editable: true,
    },
    {
      field: "jobTitle",
      headerName: "Job Title",
      width: 150,
      editable: true,
    },
    {
      field: "coverLetter",
      headerName: "Cover Letter",
      width: 150,
      editable: true,
    },
    {
        field: "resumeUrl",
        headerName: "Resume",
        width: 150,
        renderCell: (params) => <a href={`${baseUrl}/candidate/download/${params.row.resumeUrl}`} download><Download/></a>
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
    }
  ];
  const mappedCandidates = candidates.map((candidate) => {
    return {
      ...candidate,
      createdAt: new Date(candidate.createdAt).toLocaleDateString(),
      updatedAt: new Date(candidate.updatedAt).toLocaleDateString(),
    };
  });
  const rows = [...mappedCandidates];
  if (rows.length === 0) return <p>No Companies found.</p>;
  return (
    <Box sx={{ height: 400, width: "content-fit" }} className="candidates-grid">
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

export default Candidates;
