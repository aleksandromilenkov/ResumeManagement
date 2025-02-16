import React, { useEffect, useState } from 'react'
import httpModule from '../../utils/axiosHelper';
import { Button, InputLabel, TextField } from '@mui/material';
import FormControl from '@mui/material/FormControl';
import {  createEntityError, ICreateCandidateDTO, IJob } from '../../types/globalTypes';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import { useNavigate } from 'react-router-dom';
import "./candidatesPage.scss"
import Spinner from '../../utils/Spinner';

type Props = {}

const AddCandidatePage = (props: Props) => {
    const navigate = useNavigate();
    const [candidate, setCandidate] = useState<ICreateCandidateDTO>(
        {
            firstName:"",
            lastName:"",
            coverLetter:"",
            email:"",
            phone:"",
            resumeUrl: null,
            jobId: null
        }
    );
    const [jobs, setJobs] = useState<IJob[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [errorJobs, setErrorJobs] = useState<any>(null);
    const [error, setError] = useState<createEntityError | null>(null);
    const [success, setSuccess] = useState("");
    const onChangeFirstNameHandler = (e:any)=>{
        setError(null)
        setCandidate({...candidate, firstName: e.target.value})
    }
    const onChangeLastNameHandler = (e:any)=>{
        setError(null)
        setCandidate({...candidate, lastName: e.target.value})
    }
    const onChangeCoverLetterHandler = (e:any)=>{
        setError(null)
        setCandidate({...candidate, coverLetter: e.target.value})
    }
    const onChangePhoneHandler = (e:any)=>{
        setError(null)
        setCandidate({...candidate, phone: e.target.value})
    }
    const onChangeEmailHandler = (e:any)=>{
        setError(null)
        setCandidate({...candidate, email: e.target.value})
    }
    const onChangeJobHandler = (e:any)=>{
        setError(null)
        setCandidate({...candidate, jobId: +e.target.value})
    }
    const onChangeFileHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
          setCandidate({
            ...candidate,
            resumeUrl: e.target.files[0], // Store the file in the candidate object
          });
        }
      };
      
      const handleCreateCandidate = async () => {
        const phoneRegex = /^\+?\d{7,15}$/;
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        setSuccess("");
        setError(null);
        if (candidate.firstName.length <= 0 || candidate.firstName.length > 50) {
          setSuccess("");
          setError({ property: "firstName", message: "candidate first name must be between 1 and 50 characters" });
          return;
        }
        if (candidate.lastName.length <= 0 || candidate.lastName.length > 50) {
          setSuccess("");
          setError({ property: "lastName", message: "candidate lastName must be between 1 and 50 characters" });
          return;
        }
        if (candidate.coverLetter.length <= 0 || candidate.coverLetter.length > 100) {
          setSuccess("");
          setError({ property: "coverLetter", message: "candidate coverLetter must be between 1 and 100 characters" });
          return;
        }
        if (!emailRegex.test(candidate.email)) {
            setError({ property: "email", message: "Invalid email format" });
            return;
        }
        if (!phoneRegex.test(candidate.phone)) {
            setError({ property: "phone", message: "Invalid phone number. It must be between 7-15 digits and can start with +" });
            return;
        }
        if (candidate.jobId === null) {
            setSuccess("");
            setError({ property: "jobId", message: "select a valid job" });
            return;
          }
          if (candidate.resumeUrl === null) {
            setSuccess("");
            setError({ property: "resume", message: "import a valid resume" });
            return;
          }
        const formData = new FormData();
        formData.append('firstName', candidate.firstName);
        formData.append('lastName', candidate.lastName);
        formData.append('email', candidate.email);
        formData.append('phone', candidate.phone);
        formData.append('coverLetter', candidate.coverLetter);
        formData.append('resume', candidate.resumeUrl);
        formData.append('jobId', candidate.jobId.toString());
      
        try {
          const resp = await httpModule.post('/candidate', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          });
      
          if (!resp.data?.flag) {
            setError({ property: "response", message: resp.data?.message });
            return;
          }
          setSuccess("Candidate successfully created.");
          setError(null);
          setTimeout(() => navigate("/candidates"), 800);
        } catch (e: any) {
          setError({ property: "response", message: e.response.data.message });
        }
      };
      
    useEffect(()=>{
        const fetchJobs = async () => {
            try {
              setIsLoading(true);
              const response = await httpModule.get("/job");
              setJobs(response.data);
            } catch (err) {
                setErrorJobs(err);
            } finally {
              setIsLoading(false);
            }
          };
        
          fetchJobs();
    },[])
    if(isLoading) return <Spinner/>
        if(errorJobs != null) {
            return<>
             <p style={{color:'crimson'}}>Can't fetch data. Try again later</p>
             <Button variant='outlined' color='secondary' onClick={()=>navigate('/candidates')}>
                    Back
                </Button>
            </>
        }
  return (
    <div className='content'>
        <div className="add-candidate">
            <h2>Add New Candidate</h2>
            <TextField
                autoComplete='off'
                label="First Name"
                variant="outlined"
                value={candidate.firstName}
                onChange={onChangeFirstNameHandler}
            />
            {error?.property === "firstName" && <p style={{color:'crimson'}}>{error.message}</p>}
            <TextField
                autoComplete='off'
                label="Last Name"
                variant="outlined"
                value={candidate.lastName}
                onChange={onChangeLastNameHandler}
            />
            {error?.property === "lastName" && <p style={{color:'crimson'}}>{error.message}</p>}
            <TextField
                autoComplete='off'
                label="Email"
                variant="outlined"
                value={candidate.email}
                onChange={onChangeEmailHandler}
            />
            {error?.property === "email" && <p style={{color:'crimson'}}>{error.message}</p>}
            <TextField
                autoComplete='off'
                label="Phone"
                variant="outlined"
                value={candidate.phone}
                onChange={onChangePhoneHandler}
            />
            {error?.property === "phone" && <p style={{color:'crimson'}}>{error.message}</p>}
            <TextField
                autoComplete='off'
                label="Cover Letter"
                variant="outlined"
                value={candidate.coverLetter}
                onChange={onChangeCoverLetterHandler}
                multiline
            />
            {error?.property === "coverLetter" && <p style={{color:'crimson'}}>{error.message}</p>}
            <FormControl fullWidth>
                <InputLabel>Jobs</InputLabel>
                <Select
                    value={candidate.jobId}
                    label="Select Job"
                    onChange={onChangeJobHandler}
                >
                    {jobs.map((job, idx)=> <MenuItem value={job.id} key={idx}>{job.title} - {job.companyName}</MenuItem>)}
                </Select>
                {error?.property === "jobId" && <p style={{color:'crimson'}}>{error.message}</p>}
            </FormControl>
            <input
                type="file"
                accept=".pdf"
                onChange={onChangeFileHandler}
            />
            {error?.property === "resume" && <p style={{ color: 'crimson' }}>{error.message}</p>}


            <div className="btns">

            <Button variant='outlined' color='primary' onClick={handleCreateCandidate}>
                Save
            </Button>
            <Button variant='outlined' color='secondary' onClick={()=>navigate('/candidates')}>
                Back
            </Button>
            </div>
            {success.length >= 1 && <p style={{color:'green'}}>{success}</p>}
            {error?.property === "response" && <p style={{color:'crimson'}}>{error.message}</p>}
        </div>
    </div>
  )
}

export default AddCandidatePage