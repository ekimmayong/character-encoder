import React, { useState, useRef } from 'react';
import axios from 'axios';

function CharacterEncoder() {
  const [textToEncode, setTextToEncode] = useState('');
  const [encodedText, setEncodedText] = useState('');
  const [isEncoding, setIsEncoding] = useState(false);
  const cancelToken = useRef(null);

  const encodeText = async () => {
    if (!isEncoding) {
      setIsEncoding(true);
      setEncodedText('');

      cancelToken.current = axios.CancelToken.source();

      try {
        const response = await axios.post(
          'https://localhost:7068/api/home/encode',
          { text: textToEncode },
          {
            responseType: 'text',
            cancelToken: cancelToken.current.token
          }
        );
        
        const encodedBytes = Array.from(response.data);

        for (const byte of encodedBytes) {
          setEncodedText((prevText) => prevText + byte);
          await new Promise((resolve) => setTimeout(resolve, Math.floor(Math.random() * 4000) + 1000)); // 1-5 seconds pause
        }
      } catch (error) {
        if (axios.isCancel(error)) {
          setEncodedText('Encoding process canceled');
        } else {
          console.error('Error during encoding:', error.message);
        }
      } finally {
        setIsEncoding(false);
      }
    }
  };

  const cancelEncoding = async() => {
    let source = axios.CancelToken.source();
    try{
      if (cancelToken.current) {
        cancelToken.current.cancel('Encoding process canceled');
      }
    }
    catch(error){
      console.log(error)
    }
  };

  return (
    <div className='encoder-container'>
        <div>
            <div className='mb-3'>
                <label htmlFor="textToEncode" className='form-label'>Enter Text:</label>
                <input
                    type="text"
                    id="textToEncode"
                    value={textToEncode}
                    onChange={(e) => setTextToEncode(e.target.value)}
                    className='form-control'
                />
            </div>
        
            <div className='mb-3 d-flex justify-content-center'>
                <button className='btn btn-primary me-2' onClick={encodeText} disabled={isEncoding}>
                    Convert
                </button>
                <button className='btn btn-danger' onClick={cancelEncoding} disabled={!isEncoding}>
                    Cancel
                </button>
            </div>

            <div className='d-flex flex-column justify-content-center'>
                <label className='form-label text-center text-uppercase' htmlFor="encodedText">Encoded Text:</label>
                <h1 name='encodedText' className='text-center text-wrap fw-semi-bold text-decoration-underline'>
                  {encodedText}
                </h1>
            </div>
        </div>
        
    </div>
  );
}

export default CharacterEncoder;