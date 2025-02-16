import React, { useEffect, useState } from 'react'
import httpModule from '../../utils/axiosHelper'
import { IJob } from '../../types/globalTypes';
import Spinner from '../../utils/Spinner';
import { AxiosError } from 'axios';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Add } from '@mui/icons-material';
import "./jobsPage.scss";
import Jobs from '../../components/Jobs/Jobs';

type Props = {}

const JobsPage = (props: Props) => {
  const [jobs, setJobs] = useState<IJob[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<AxiosError | any>();
  const navigate = useNavigate();
  console.log(error);
  useEffect(()=>{
    const fetchJobs = async () => {
        try {
          setIsLoading(true);
          const response = await httpModule.get("/job");
          setJobs(response.data);
        } catch (err) {
          setError(err);
        } finally {
          setIsLoading(false);
        }
      };
    
      fetchJobs();
  },[])
  if(isLoading) return <Spinner/>
  if(error) return <p>{error.message}</p>
  return (
    <div className='content jobs'>
        <div className="heading">
            <h2>Jobs</h2>
            <Button variant='outlined' onClick={()=> navigate("/jobs/add")}>
                <Add/>
            </Button>
        </div>
        <Jobs jobs={jobs}/>
    </div>
  )
}

export default JobsPage