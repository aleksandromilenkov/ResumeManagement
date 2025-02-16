import React, { useEffect, useState } from 'react'
import {  jobLevel } from '../../types/globalEnums'
import httpModule from '../../utils/axiosHelper';
import { Button, InputLabel, TextField } from '@mui/material';
import FormControl from '@mui/material/FormControl';
import {  createEntityError, ICompany, ICreateCompanyDTO, ICreateJobDTO } from '../../types/globalTypes';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import { useNavigate } from 'react-router-dom';
import "./jobsPage.scss"
import Spinner from '../../utils/Spinner';
type Props = {}

const AddJobPage = (props: Props) => {
    const navigate = useNavigate();
    const optionsJobLevel = Object.keys(jobLevel).filter((value) =>
        isNaN(Number(value))
        );
    const [job, setJob] = useState<ICreateJobDTO>({title:"", companyId: 0, level:""});
    const [companies, setCompanies] = useState<ICompany[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<createEntityError | null>(null);
    const [errorCompanies, setErrorCompanies] = useState<any>(null);
    const [success, setSuccess] = useState("");
    const onChangeJobTitleHandler = (e:any)=>{
        setError(null)
        setJob({...job, title: e.target.value})
    }
    const handleCreateJob = async()=>{
        if(job.title.length <= 0 || job.title.length > 50){
            setSuccess("");
            setError({property: "title", message:"job title must be between 1 and 50 characters"});
            return;
        } 
        if(companies.every(c => c.id != job.companyId)){
            setSuccess("");
            setError({property: "companyId", message:`Please select a valid company`});
            return;
        }
        if(optionsJobLevel.every(level => level != job.level)){
            setSuccess("");
            setError({property: "level", message:`Level must be one of: ${optionsJobLevel.join(', ')} `});
            return;
        }
        try{
            const resp = await httpModule.post('/job',job);
            if(!resp.data?.flag){
                setError({property: "response", message: resp.data?.message});
                return;
            }
            setSuccess("Job successfully created.");
            setError(null);
            setTimeout(()=>navigate("/jobs"), 800)
            
        }catch(e:any){
            setError({property: "response", message: e.response.data.message});
        }
    }
    useEffect(()=>{
        const fetchCompanies = async () => {
            try {
              setIsLoading(true);
              const response = await httpModule.get("/company");
              setCompanies(response.data);
            } catch (err) {
                setErrorCompanies(err);
            } finally {
              setIsLoading(false);
            }
          };
        
          fetchCompanies();
    },[])
    if(isLoading) return <Spinner/>
    if(errorCompanies != null) {
        return<>
         <p style={{color:'crimson'}}>Can't fetch data. Try again later</p>
         <Button variant='outlined' color='secondary' onClick={()=>navigate('/jobs')}>
                Back
            </Button>
        </>
    }
  return (
    <div className='content'>
        <div className="add-job">
            <h2>Add New Job</h2>
            <TextField
                autoComplete='off'
                label="Job Title"
                variant="outlined"
                value={job.title}
                onChange={onChangeJobTitleHandler}
            />
            {error?.property === "title" && <p style={{color:'crimson'}}>{error.message}</p>}
            <FormControl fullWidth>
                <InputLabel>Select Company</InputLabel>
                <Select
                    value={job.companyId}
                    label="Select Size"
                    onChange={(e)=> setJob({...job, companyId: +e.target.value})}
                >
                    {companies.map((company, idx)=> <MenuItem value={company.id} key={idx}>{company.name}</MenuItem>)}
                </Select>
                {error?.property === "companyId" && <p style={{color:'crimson'}}>{error.message}</p>}
            </FormControl>
            <FormControl fullWidth>
                <InputLabel>Select Level</InputLabel>
                <Select
                    value={job.level}
                    label="Select Level"
                    onChange={(e)=> setJob({...job, level: e.target.value})}
                >
                    {optionsJobLevel.map((level, idx)=> <MenuItem value={level} key={idx}>{level}</MenuItem>)}
                </Select>
                {error?.property === "level" && <p style={{color:'crimson'}}>{error.message}</p>}
            </FormControl>
            <div className="btns">

            <Button variant='outlined' color='primary' onClick={handleCreateJob} disabled={error != null}>
                Save
            </Button>
            <Button variant='outlined' color='secondary' onClick={()=>navigate('/jobs')}>
                Back
            </Button>
            </div>
            {success.length >= 1 && <p style={{color:'green'}}>{success}</p>}
            {error?.property === "response" && <p style={{color:'crimson'}}>{error.message}</p>}
        </div>
    </div>
  )
}

export default AddJobPage