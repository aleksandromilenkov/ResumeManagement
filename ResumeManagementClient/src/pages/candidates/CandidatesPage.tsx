import React, { useEffect, useState } from 'react'
import { ICandidate } from '../../types/globalTypes';
import { AxiosError } from 'axios';
import { useNavigate } from 'react-router-dom';
import httpModule from '../../utils/axiosHelper';
import Spinner from '../../utils/Spinner';
import { Button } from '@mui/material';
import { Add } from '@mui/icons-material';
import Candidates from '../../components/Candidates/Candidates';
import "./candidatesPage.scss"
type Props = {}

const CandidatesPage = (props: Props) => {
    const [candidates, setCandidates] = useState<ICandidate[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<AxiosError | any>();
    const navigate = useNavigate();
    console.log(error);
    useEffect(()=>{
      const fetchCandidates = async () => {
          try {
            setIsLoading(true);
            const response = await httpModule.get("/candidate");
            setCandidates(response.data);
          } catch (err) {
            setError(err);
          } finally {
            setIsLoading(false);
          }
        };
      
        fetchCandidates();
    },[])
    if(isLoading) return <Spinner/>
    if(error) return <p>{error.message}</p>
    return (
      <div className='content candidates'>
          <div className="heading">
              <h2>Candidates</h2>
              <Button variant='outlined' onClick={()=> navigate("/candidates/add")}>
                  <Add/>
              </Button>
          </div>
          <Candidates candidates={candidates}/>
      </div>
    )
}

export default CandidatesPage