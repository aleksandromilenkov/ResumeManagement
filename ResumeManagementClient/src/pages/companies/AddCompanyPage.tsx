import React, { useState } from 'react'
import { companySize } from '../../types/globalEnums'
import httpModule from '../../utils/axiosHelper';
import { Button, InputLabel, TextField } from '@mui/material';
import FormControl from '@mui/material/FormControl';
import {  createCompanyError, ICreateCompanyDTO } from '../../types/globalTypes';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import { useNavigate } from 'react-router-dom';
import "./companiesPage.scss"
import { AxiosError } from 'axios';
type Props = {}

const AddCompanyPage = (props: Props) => {
    const navigate = useNavigate();
    const optionsCompanySize = Object.keys(companySize).filter((value) =>
        isNaN(Number(value))
        );
    const [company, setCompany] = useState<ICreateCompanyDTO>({name:"", size: ""});
    const [error, setError] = useState<createCompanyError | null>(null);
    const [success, setSuccess] = useState("");
    const onChangeCompanyHandler = (e:any)=>{
        setError(null)
        setCompany({...company, name: e.target.value})
    }
    const handleCreateCompany = async()=>{
        if(company.name.length <= 0 || company.name.length > 20){
            setSuccess("");
            setError({property: "name", message:"company name must be between 1 and 20 characters"});
            return;
        } 
        if(optionsCompanySize.every(size => size != company.size)){
            setSuccess("");
            setError({property: "size", message:"Size must be small, medium or large"});
            return;
        }
        try{

            const resp = await httpModule.post('/company',company);
            if(!resp.data?.flag){
                setError({property: "response", message: resp.data?.message});
                return;
            }
            setSuccess("Company successfully created.");
            setError(null);
            setTimeout(()=>navigate("/companies"), 800)
            
        }catch(e:any){
            setError({property: "response", message: e.response.data.message});
        }
    }
  return (
    <div className='content'>
        <div className="add-company">
            <h2>Add New Company</h2>
            <TextField
                autoComplete='off'
                label="Company Name"
                variant="outlined"
                value={company.name}
                onChange={onChangeCompanyHandler}
            />
            {error?.property === "name" && <p style={{color:'crimson'}}>{error.message}</p>}
            <FormControl fullWidth>
                <InputLabel>Company Size</InputLabel>
                <Select
                    value={company.size}
                    label="Select Size"
                    onChange={(e)=> setCompany({...company, size: e.target.value})}
                >
                    {optionsCompanySize.map((size, idx)=> <MenuItem value={size} key={idx}>{size}</MenuItem>)}
                </Select>
                {error?.property === "size" && <p style={{color:'crimson'}}>{error.message}</p>}
            </FormControl>
            <div className="btns">

            <Button variant='outlined' color='primary' onClick={handleCreateCompany}>
                Save
            </Button>
            <Button variant='outlined' color='secondary' onClick={()=>navigate('/companies')}>
                Back
            </Button>
            </div>
            {success.length >= 1 && <p style={{color:'green'}}>{success}</p>}
            {error?.property === "response" && <p style={{color:'crimson'}}>{error.message}</p>}
        </div>
    </div>
  )
}

export default AddCompanyPage